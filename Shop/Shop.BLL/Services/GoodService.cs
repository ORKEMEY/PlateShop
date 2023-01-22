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
	public class GoodService : IGoodService
	{

		protected IUnitOfWork uof { get; set; }

		public GoodService(IUnitOfWork uof)
		{
			this.uof = uof;
		}

		public void AddItem(GoodDTO goodDTO)
		{
			if (String.IsNullOrEmpty(goodDTO.Name))
				throw new ValidationException("Wrong or empty properties", "Name");

			var goodDAL = MapperBLL.Mapper.Map<Good>(goodDTO);
			uof.Goods.Create(goodDAL);
			uof.Save();
		}

		public void DeleteItem(GoodDTO goodDTO)
		{
			if (goodDTO.Id <= 0)
				throw new ValidationException("Wrong or empty property Id");
			uof.Goods.Delete(goodDTO.Id);
			uof.Save();
		}

		public void UpdateItem(GoodDTO goodDTO)
		{
			if (goodDTO.Id <= 0 | goodDTO.Name == null)
				throw new ValidationException("Wrong or empty properties");

			var goodDAL = uof.Goods.GetItem(goodDTO.Id);
			if (goodDAL == null) throw new ValidationException("Item not found");

			goodDAL.Name = goodDTO.Name;
			goodDAL.Description = goodDTO.Description;

			uof.Goods.Update(goodDAL);
			uof.Save();
		}

		public IEnumerable<GoodDTO> GetItems()
		{
			IEnumerable<Good> items = uof.Goods.GetItems(c => true);
			return MapperBLL.Mapper.Map<IEnumerable<GoodDTO>>(items);

		}

		public IEnumerable<GoodDTO> GetItems(string name)
		{
			IEnumerable<Good> items = uof.Goods.GetItems(c => c.Name == name);
			return MapperBLL.Mapper.Map<IEnumerable<GoodDTO>>(items);
		}

		public GoodDTO GetItem(int? id)
		{
			if (id == null) throw new ValidationException("Id of question isn't set", "id");
			var good = uof.Goods.GetItem(id.Value);

			if (good == null) throw new ValidationException("No question was found");
			return MapperBLL.Mapper.Map<GoodDTO>(good);

		}
	}
}
