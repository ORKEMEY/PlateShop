using System;
using System.Collections.Generic;

namespace Shop.BLL.DTO
{
	public class UserDTO
	{
		public int Id { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }

		public string Role { get; set; }

		public IEnumerable<OrderDTO> Orders { get; set; }

		public string RefreshToken { get; set; }
		public DateTime? RefreshTokenExpiryTime { get; set; }

		public UserDTO()
		{
			Orders = new List<OrderDTO>();
		}
	}
}