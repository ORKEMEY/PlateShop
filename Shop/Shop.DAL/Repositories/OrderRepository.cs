using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Shop.DAL.Models;
using System.Linq.Expressions;
using System.Linq;

namespace Shop.DAL.Repositories
{
	class OrderRepository : IRepository<Order>
	{

		protected ShopContext db { get; set; }

		public OrderRepository(ShopContext context)
		{
			db = context;
		}

		public Order GetItem(int id)
		{
			return db.Orders.Include(c => c.Goods)
				.FirstOrDefault(x => x.Id == id);

		}

		public void Create(Order item)
		{
			db.Orders.Add(item);
		}

		public void Update(Order item)
		{
			db.Entry(item).State = EntityState.Modified;
		}

		public void Delete(int id)
		{
			Order item = db.Orders.Find(id);
			if (item != null)
				db.Orders.Remove(item);
		}

		public void Save() => db.SaveChanges();

		public IEnumerable<Order> GetItems(Expression<Func<Order, bool>> predicate)
		{
			return db.Orders.Include(c => c.Goods).Where(predicate);
		}

	}
}