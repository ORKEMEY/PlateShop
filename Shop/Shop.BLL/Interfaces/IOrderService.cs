using System;
using System.Collections.Generic;
using Shop.BLL.DTO;
using Shop.BLL.Infrastructure;

namespace Shop.BLL.Interfaces
{
	public interface IOrderService : ICRUDService<OrderDTO>
	{
		IEnumerable<OrderDTO> GetItems(string name);
		IEnumerable<OrderDTO> GetItemsByUserId(int? id);
		void AddGood(OrderDTO goodDTO);
		void DeleteGood(OrderDTO goodDTO);
	}
}
