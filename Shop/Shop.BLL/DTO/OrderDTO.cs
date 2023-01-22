using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.BLL.DTO
{
	public class OrderDTO
	{
		public int Id { get; set; }
		public string Address { get; set; }

		public int UserId { get; set; }
		public UserDTO User { get; set; }

		public IEnumerable<GoodDTO> Goods { get; set; }

		public OrderDTO()
		{
			Goods = new List<GoodDTO>();
		}

	}
}
