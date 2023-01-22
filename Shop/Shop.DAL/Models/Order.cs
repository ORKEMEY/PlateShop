using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.DAL.Models
{
	public class Order
	{

		public int Id { get; set; }
		public string Address { get; set; }

		public int UserId { get; set; }
		public User User { get; set; }

		public IEnumerable<Good> Goods { get; set; }

		public Order()
		{
			Goods = new List<Good>();
		}

	}
}
