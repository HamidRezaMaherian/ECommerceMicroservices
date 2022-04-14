using AutoMapper;
using Order.Application.DTOs;
using Order.Domain.Entities;

namespace Order.Application.Configurations;

public class ServiceMapper : Profile
{
	public ServiceMapper()
	{
		CreateMap<OrderDTO,Domain.Entities.Order>().ReverseMap();
		CreateMap<DeliveryDTO,Delivery>().ReverseMap();
		CreateMap<PaymentDTO, Payment>().ReverseMap();
		CreateMap<OrderItemDTO, OrderItem>().ReverseMap();
	}
}