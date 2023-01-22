using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.BLL.DTO
{
	public class GoodDTO
	{

		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public double Cost { get; set; }

		public IEnumerable<OrderDTO> Orders { get; set; }

		public GoodDTO()
		{
			Orders = new List<OrderDTO>();
		}
	}
}
