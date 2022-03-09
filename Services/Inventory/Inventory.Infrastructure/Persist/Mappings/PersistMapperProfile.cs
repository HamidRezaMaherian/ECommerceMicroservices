using AutoMapper;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.Persist.DAOs;

namespace Inventory.Infrastructure.Persist.Mappings
{
	public class PersistMapperProfile : Profile
	{
		public PersistMapperProfile()
		{
			CreateMap<StockDAO, Stock>().ReverseMap();
			CreateMap<StoreDAO, Store>().ReverseMap();
		}
	}
}
