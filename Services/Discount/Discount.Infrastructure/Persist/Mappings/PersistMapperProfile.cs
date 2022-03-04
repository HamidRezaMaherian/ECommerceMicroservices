using AutoMapper;
using Discount.Domain.Entities;
using Discount.Infrastructure.Persist.DAOs;

namespace Discount.Infrastructure.Persist.Mappings
{
	public class PersistMapperProfile : Profile
	{
		public PersistMapperProfile()
		{
			CreateMap<PercentDiscountDAO, PercentDiscount>().ReverseMap();
			CreateMap<PriceDiscountDAO, PriceDiscount>().ReverseMap();
		}
	}
}
