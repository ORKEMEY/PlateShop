using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Shop.DAL.Models;
using System.Linq.Expressions;
using System.Linq;

namespace Shop.DAL.Repositories
{
	class GoodRepository : IRepository<Good>
	{

		protected ShopContext db { get; set; }

		public GoodRepository(ShopContext context)
		{
			db = context;
		}

		public Good GetItem(int id)
		{
			return db.Goods
				.FirstOrDefault(x => x.Id == id);

		}

		public void Create(Good item)
		{
			db.Goods.Add(item);
		}

		public void Update(Good item)
		{
			db.Entry(item).State = EntityState.Modified;
		}

		public void Delete(int id)
		{
			Good item = db.Goods.Find(id);
			if (item != null)
				db.Goods.Remove(item);
		}

		public void Save() => db.SaveChanges();

		public IEnumerable<Good> GetItems(Expression<Func<Good, bool>> predicate)
		{
			return db.Goods.Where(predicate);
		}

	}
}