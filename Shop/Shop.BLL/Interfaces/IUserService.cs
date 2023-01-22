using System;
using System.Collections.Generic;
using Shop.BLL.DTO;

namespace Shop.BLL.Interfaces
{
	public interface IUserService : ICRUDService<UserDTO>
	{
		IEnumerable<UserDTO> GetItems(string login);
		public UserDTO Authentificate(string login, string password);
		public TokenDTO RefreshToken(TokenDTO token);
	}
}
