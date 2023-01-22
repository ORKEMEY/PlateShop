using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.BLL.Interfaces
{
	public interface ICRUDService<T> where T : class
	{
		void AddItem(T item);
		void DeleteItem(T item);
		void UpdateItem(T item);
		IEnumerable<T> GetItems();
		T GetItem(int? id);
	}
}
