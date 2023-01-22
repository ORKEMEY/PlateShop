using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.PL.Models
{
	public class UserViewModel
	{
		public int Id { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public string Role { get; set; } = "Client";
	}
}
