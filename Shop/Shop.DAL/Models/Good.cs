using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.DAL.Models
{
	public class Good
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public double Cost { get; set; }

		public IEnumerable<Order> Orders { get; set; }

		public Good()
		{
			Orders = new List<Order>();
		}
	}
}
