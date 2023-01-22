using Shop.BLL.DTO;
using Shop.DAL.Models;
using AutoMapper;

namespace Shop.BLL
{
	class MapperBLL
	{
		public static MapperConfiguration MapperConf { get; private set; }
		public static Mapper Mapper { get; private set; }

		static MapperBLL()
		{
			MapperConf = new MapperConfiguration(cfg =>
			{


				cfg.CreateMap<User, UserDTO>();
				cfg.CreateMap<UserDTO, User>();

				cfg.CreateMap<Order, OrderDTO>();
				cfg.CreateMap<OrderDTO, Order>();

				cfg.CreateMap<Good, GoodDTO>();
				cfg.CreateMap<GoodDTO, Good>();

			}
			);
			Mapper = new Mapper(MapperConf);
		}
	}
}