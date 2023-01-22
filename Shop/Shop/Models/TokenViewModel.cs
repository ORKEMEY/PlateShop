using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.PL.Models
{
	public class TokenViewModel
	{
		public string AccessToken { get; set; }

		public string RefreshToken { get; set; }

		public string Login { get; set; }

		public int UserId { get; set; }

		public string Role { get; set; }

	}
}
