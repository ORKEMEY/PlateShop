using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.DAL.Models
{
	public class User
	{
		public int Id { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public string Role { get; set; }

		public IEnumerable<Order> Orders { get; set; }

		public string RefreshToken { get; set; }
		public DateTime? RefreshTokenExpiryTime { get; set; }

		public User()
		{
			Role = "Client";
			Orders = new List<Order>();
		}

	}
}
