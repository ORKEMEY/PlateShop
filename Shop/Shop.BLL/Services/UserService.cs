using System;
using System.Collections.Generic;
using Shop.BLL.Interfaces;
using Shop.BLL.Infrastructure;
using Shop.BLL.DTO;
using Shop.DAL.Models;
using Shop.DAL;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Shop.BLL.Services
{
	public class UserService : IUserService
	{
		protected IUnitOfWork uof { get; set; }
		

		public UserService(IUnitOfWork uof)
		{
			this.uof = uof;
		}

		public void AddItem(UserDTO userDTO)
		{
			if (String.IsNullOrEmpty(userDTO.Login))
				throw new ValidationException("Wrong or empty properties", "Login");
			if (String.IsNullOrEmpty(userDTO.Password))
				throw new ValidationException("Wrong or empty properties", "Password");
			if (String.IsNullOrEmpty(userDTO.Role))
				userDTO.Role = "Client";

			var user = uof.Users.GetItems(u => u.Login == userDTO.Login).FirstOrDefault();
			if(user != null)
				throw new ValidationException("This Login is already taken", "Login");

			HashPassword(userDTO);
			AssignRefreshToken(userDTO);

			var userDAL = MapperBLL.Mapper.Map<User>(userDTO);

			uof.Users.Create(userDAL);
			uof.Save();
		}

		public void DeleteItem(UserDTO userDTO)
		{
			if (userDTO.Id <= 0)
				throw new ValidationException("Wrong or empty property Id");
			uof.Users.Delete(userDTO.Id);
			uof.Save();
		}

		public void UpdateItem(UserDTO userDTO)
		{
			if (userDTO.Id <= 0)
				throw new ValidationException("Wrong or empty properties");

			var userDAL = uof.Users.GetItem(userDTO.Id);
			if (userDAL == null) throw new ValidationException("Item not found");

			if (userDTO.Login != null)
				userDAL.Login = userDTO.Login;
			if (userDTO.Password != null)
			{
				HashPassword(userDTO);
				userDAL.Password = userDTO.Password;
			}

			uof.Users.Update(userDAL);
			uof.Save();
		}

		public IEnumerable<UserDTO> GetItems()
		{
			IEnumerable<User> items = uof.Users.GetItems(c => true);
			return MapperBLL.Mapper.Map<IEnumerable<UserDTO>>(items);

		}


		public IEnumerable<UserDTO> GetItems(string login)
		{
			IEnumerable<User> items = uof.Users.GetItems(c => c.Login == login);
			return MapperBLL.Mapper.Map<IEnumerable<UserDTO>>(items);
		}

		public UserDTO Authentificate(string login, string password)
		{

			var searchedUser = new UserDTO()
			{
				Login = login,
				Password = password
			};
			UserDTO userDTO = null;

			try { 
				HashPassword(searchedUser);

				User item = uof.Users.GetItems(u => u.Login == searchedUser.Login & u.Password == searchedUser.Password).FirstOrDefault();

				userDTO =  MapperBLL.Mapper.Map<UserDTO>(item);
				AssignRefreshToken(userDTO);

				item.RefreshToken = userDTO.RefreshToken;
				item.RefreshTokenExpiryTime = userDTO.RefreshTokenExpiryTime;

				uof.Users.Update(item);
				uof.Save();
			}
			catch (ArgumentNullException)
			{
			}
			
			return userDTO;
		}

		public UserDTO GetItem(int? id)
		{
			if (id == null) throw new ValidationException("Id of user isn't set", "id");
			var user = uof.Users.GetItem(id.Value);

			if (user == null) throw new ValidationException("No user was found");
			return MapperBLL.Mapper.Map<UserDTO>(user);
		}


		/// <exception cref="ValidationException"></exception>
		public TokenDTO RefreshToken(TokenDTO token)
		{

			try
			{
				var principal = AuthOptions.GetPrincipalFromExpiredToken(token.AccessToken);

				User user = uof.Users.GetItems(u => u.Login == principal.Identity.Name).FirstOrDefault();
				if (user == null) throw new ValidationException("No user was found");
				
				if(user.RefreshToken == null || user.RefreshToken != token.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now) 
					throw new ValidationException("Refresh token isn't valid, reauthentificate");

				token.AccessToken = AuthOptions.GenerateAccessToken(principal.Claims);
				token.Login = user.Login;
				token.Role = user.Role;

			}
			catch (SecurityTokenException)
			{
				throw new ValidationException("Access token isn't valid, reauthentificate");
			}
			catch (Exception)
			{
				throw new ValidationException("Wrong or empty properties");
			}

			return token;
		}

		private void HashPassword(UserDTO user)
		{
			if (user == null || user?.Password == null)
			{
				throw new ArgumentNullException();
			}

			using (var sha = new SHA256Managed())
			{
				byte[] textBytes = Encoding.UTF8.GetBytes(user?.Password);
				byte[] hashBytes = sha.ComputeHash(textBytes);

				string hash = BitConverter
					.ToString(hashBytes)
					.Replace("-", String.Empty);

				user.Password = hash;
			}

		}

		private void AssignRefreshToken(UserDTO user)
		{
			if (user == null || user?.Password == null)
			{
				throw new ArgumentNullException();
			}

			using (var sha = new SHA256Managed())
			{
				byte[] textBytes = Encoding.UTF8.GetBytes(user?.Password + DateTime.Now.ToString());
				byte[] hashBytes = sha.ComputeHash(textBytes);

				string hash = BitConverter
					.ToString(hashBytes)
					.Replace("-", String.Empty);

				user.RefreshToken = hash;
				user.RefreshTokenExpiryTime = DateTime.Now.AddDays(10);
			}

		}

	}
}
