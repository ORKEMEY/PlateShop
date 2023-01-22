using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Shop.DAL.Models;
using Shop.DAL.Repositories;
using System.Linq.Expressions;
using System.Linq;


namespace Shop.DAL
{
	public interface IUnitOfWork : IDisposable
	{
		IRepository<User> Users { get; }
		IRepository<Order> Orders { get; }
		IRepository<Good> Goods { get; }
		void Save();
	}
}