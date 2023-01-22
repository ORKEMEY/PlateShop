using System;
using System.Collections.Generic;
using Shop.BLL.Interfaces;
using Shop.BLL.Infrastructure;
using Shop.BLL.DTO;
using Shop.DAL.Models;
using Shop.DAL;
using System.Linq;


namespace Shop.BLL.Services
{
	
	public class OrderService : IOrderService
	{
		protected IUnitOfWork uof { get; set; }
		protected IGoodService goodService { get; set; }
		protected IUserService userService { get; set; }

		public OrderService(IUnitOfWork uof, IGoodService gs, IUserService us)
		{
			this.uof = uof;
			this.goodService = gs;
			this.userService = us;
		}

		public void AddItem(OrderDTO orderDTO)
		{
			if (String.IsNullOrEmpty(orderDTO.Address))
				throw new ValidationException("Wrong or empty properties", "Address");
			
			var orderDAL = MapperBLL.Mapper.Map<Order>(orderDTO);

			(orderDAL.Goods as List<Good>).Clear();

			foreach(var good in orderDTO.Goods)
			{
				(orderDAL.Goods as List<Good>).Add(uof.Goods.GetItem(good.Id));
			}

			uof.Orders.Create(orderDAL);
			uof.Save();
		}

		public void DeleteItem(OrderDTO orderDTO)
		{
			if (orderDTO.Id <= 0)
				throw new ValidationException("Wrong or empty property Id");
			uof.Orders.Delete(orderDTO.Id);
			uof.Save();
		}

		public void UpdateItem(OrderDTO orderDTO)
		{
			if (orderDTO.Id <= 0)
				throw new ValidationException("Wrong or empty properties");

			var orderDAL = uof.Orders.GetItem(orderDTO.Id);
			if (orderDAL == null) throw new ValidationException("Item not found");

			if(orderDTO.Address != null)
				orderDAL.Address = orderDTO.Address;
			
			uof.Orders.Update(orderDAL);
			uof.Save();
		}

		public void AddGood(OrderDTO orderDTO)
		{
			if (orderDTO?.Id <= 0 || orderDTO?.Goods?.First().Id <= 0)
				throw new ValidationException("Wrong or empty properties");

			var gDAL = uof.Goods.GetItem(orderDTO.Goods.First().Id);
			if (gDAL == null)
				throw new ValidationException("Good not found");
			var orderDAL = uof.Orders.GetItem(orderDTO.Id);
			if (orderDAL == null)
				throw new ValidationException("Order not found");


			(orderDAL.Goods as List<Good>).Add(gDAL);

			uof.Save();

		}

		public void DeleteGood(OrderDTO orderDTO)
		{
			if (orderDTO?.Id <= 0 || orderDTO?.Goods?.First().Id <= 0)
				throw new ValidationException("Wrong or empty properties");

			var gDAL = uof.Goods.GetItem(orderDTO.Goods.First().Id);
			if (gDAL == null)
				throw new ValidationException("Good not found");
			var orderDAL = uof.Orders.GetItem(orderDTO.Id);
			if (orderDAL == null)
				throw new ValidationException("Order not found");

			(orderDAL.Goods as List<Good>).Remove(gDAL);

			uof.Save();

		}

		public IEnumerable<OrderDTO> GetItems()
		{
			IEnumerable<Order> items = uof.Orders.GetItems(c => true);
			return MapperBLL.Mapper.Map<IEnumerable<OrderDTO>>(items);
		}

		public IEnumerable<OrderDTO> GetItems(string address)
		{
			if (String.IsNullOrEmpty(address))
				throw new ValidationException("Wrong or empty property Id");

			IEnumerable<Order> items = uof.Orders.GetItems(c => c.Address == address);
			return MapperBLL.Mapper.Map<IEnumerable<OrderDTO>>(items);
		}

		public OrderDTO GetItem(int? id)
		{
			if (id == null) throw new ValidationException("Id of order isn't set", "id");
			var order = uof.Orders.GetItem(id.Value);

			if (order == null) throw new ValidationException("No order was found");
			return MapperBLL.Mapper.Map<OrderDTO>(order);

		}

		public IEnumerable<OrderDTO> GetItemsByUserId(int? id)
		{
			if (id == null) throw new ValidationException("Id of user isn't set", "id");
			var orders = uof.Orders.GetItems(c => c.UserId == id);

			if (orders == null) throw new ValidationException("No orders was found");
			return MapperBLL.Mapper.Map<List<OrderDTO>>(orders);

		}

	}

	
}
