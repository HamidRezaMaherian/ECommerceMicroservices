using AutoMapper;
using Discount.Domain.Common;

namespace Discount.Configurations.Valiations
{
	public class ApiMapperProfile : Profile
	{
		public ApiMapperProfile()
		{
			CreateMap<DiscountRPC.DiscountBaseResult, DiscountBase>().ReverseMap();
		}
	}
}