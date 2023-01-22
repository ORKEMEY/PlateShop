using System;
using Microsoft.EntityFrameworkCore;
using Shop.DAL.Repositories;
using Shop.DAL.Models;

namespace Shop.DAL
{
	public class UnitOfWork : IUnitOfWork
	{
		private ShopContext db { get; set; }

		private UserRepository _userRepository { get; set; }
		private GoodRepository _goodRepository { get; set; }
		private OrderRepository _orderRepository { get; set; }

		public UnitOfWork(string connectionString)
		{

			var optionsBuilder = new DbContextOptionsBuilder<ShopContext>();

			var options = optionsBuilder.UseSqlServer(connectionString).Options;

			db = new ShopContext(options);
		}

		public UnitOfWork(DbContextOptions options)
		{
			db = new ShopContext(options);
		}

		
		public IRepository<User> Users
		{
			get
			{
				if (_userRepository == null)
					_userRepository = new UserRepository(db);
				return _userRepository;
			}
		}

		public IRepository<Good> Goods
		{
			get
			{
				if (_goodRepository == null)
					_goodRepository = new GoodRepository(db);
				return _goodRepository;
			}
		}

		public IRepository<Order> Orders
		{
			get
			{
				if (_orderRepository == null)
					_orderRepository = new OrderRepository(db);
				return _orderRepository;
			}
		}


		public void Save() => db.SaveChanges();


		private bool disposed = false;

		public virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					db.Dispose();


				}
				this.disposed = true;
			}

		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}