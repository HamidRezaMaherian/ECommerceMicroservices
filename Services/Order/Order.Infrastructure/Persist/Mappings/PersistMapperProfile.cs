using AutoMapper;
using Order.Domain.Entities;
using Order.Infrastructure.Persist.DAOs;

namespace Order.Infrastructure.Persist.Mappings
{
	public class PersistMapperProfile : Profile
	{
		public PersistMapperProfile()
		{
			CreateMap<Domain.Entities.Order, OrderDAO>().ReverseMap();
			CreateMap<Payment, PaymentDAO>().ReverseMap().ForMember(i => i.Id, opt => opt.MapFrom(i => i.OrderId));
			CreateMap<Delivery, DeliveryDAO>().ReverseMap().ForMember(i => i.Id, opt => opt.MapFrom(i => i.OrderId));
			CreateMap<OrderItem, OrderItemDAO>().ReverseMap();
		}
	}
}
