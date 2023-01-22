using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Shop.DAL.Models;
using System.Linq.Expressions;
using System.Linq;

namespace Shop.DAL.Repositories
{
	class UserRepository : IRepository<User>
	{

		protected ShopContext db { get; set; }

		public UserRepository(ShopContext context)
		{
			db = context;
		}

		public User GetItem(int id)
		{
			return db.Users
				.FirstOrDefault(x => x.Id == id);

		}

		public void Create(User item)
		{
			db.Users.Add(item);
		}

		public void Update(User item)
		{
			db.Entry(item).State = EntityState.Modified;
		}

		public void Delete(int id)
		{
			User item = db.Users.Find(id);
			if (item != null)
				db.Users.Remove(item);
		}

		public void Save() => db.SaveChanges();

		public IEnumerable<User> GetItems(Expression<Func<User, bool>> predicate)
		{
			return db.Users.Where(predicate).Include(u => u.Orders);
		}

	}
}