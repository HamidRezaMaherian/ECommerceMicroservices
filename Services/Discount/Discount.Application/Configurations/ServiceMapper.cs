using AutoMapper;
using Discount.Application.DTOs;
using Discount.Domain.Entities;

namespace Discount.Application.Configurations;

public class ServiceMapper : Profile
{
	public ServiceMapper()
	{
		CreateMap<PercentDiscountDTO, PercentDiscount>().ReverseMap();
		CreateMap<PriceDiscountDTO, PriceDiscount>().ReverseMap();
	}
}