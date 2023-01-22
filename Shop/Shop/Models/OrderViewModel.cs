using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.PL.Models
{
	public class OrderViewModel
	{

		public int Id { get; set; }
		public string Address { get; set; }

		public int UserId { get; set; }

		public IEnumerable<GoodViewModel> Goods { get; set; }

		public OrderViewModel()
		{
			Goods = new List<GoodViewModel>();
		}
	}
}
