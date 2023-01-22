using Shop.BLL.DTO;
using Shop.DAL.Models;
using AutoMapper;
using Shop.PL.Models;

namespace Shop.PL
{
	public class MapperWEB
	{
		public static MapperConfiguration MapperConf { get; private set; }
		public static Mapper Mapper { get; private set; }

		static MapperWEB()
		{
			MapperConf = new MapperConfiguration(cfg =>
			{

			cfg.CreateMap<OrderViewModel, OrderDTO>();
				cfg.CreateMap<OrderDTO, OrderViewModel>()
				.ForMember("Id", opt => opt.MapFrom(src => src.Id))
				.ForMember("Address", opt => opt.MapFrom(src => src.Address))
				.ForMember("UserId", opt => opt.MapFrom(src => src.UserId))
				.ForMember("Goods", opt => opt.MapFrom(src => src.Goods));

				cfg.CreateMap<GoodViewModel, GoodDTO>();
				cfg.CreateMap<GoodDTO, GoodViewModel>();

				cfg.CreateMap<UserViewModel, UserDTO>();
				cfg.CreateMap<UserDTO, UserViewModel>();

				cfg.CreateMap<TokenDTO, TokenViewModel>();
				cfg.CreateMap<TokenViewModel, TokenDTO>();

			}
			);
			Mapper = new Mapper(MapperConf);
		}

	}
}
