using AutoMapper;
using Inventory.Application.DTOs;
using Inventory.Domain.Entities;

namespace Inventory.Application.Configurations;

public class ServiceMapper : Profile
{
	public ServiceMapper()
	{
		CreateMap<StoreDTO, Store>().ReverseMap();
		CreateMap<StockDTO, Stock>().ReverseMap();
	}
}