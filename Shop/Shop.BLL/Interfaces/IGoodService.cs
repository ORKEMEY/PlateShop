using System;
using System.Collections.Generic;
using Shop.BLL.DTO;

namespace Shop.BLL.Interfaces
{
	public interface IGoodService : ICRUDService<GoodDTO>
	{
		IEnumerable<GoodDTO> GetItems(string query);
	}
}
