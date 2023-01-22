using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.BLL.DTO
{
	public class TokenDTO
	{
		public string AccessToken { get; set; }

		public string RefreshToken { get; set; }

		public string Login { get; set; }

		public int UserId { get; set; }

		public string Role { get; set; }
	}
}
