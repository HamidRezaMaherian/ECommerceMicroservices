using AutoMapper;
using Order.Domain.Entities;
using Order.Infrastructure.Persist.DAOs;

namespace Order.Infrastructure.Persist.Mappings
{
	public class PersistMapperProfile : Profile
	{
		public PersistMapperProfile()
		{
			CreateMap<Domain.Entities.Order,OrderDAO>().ReverseMap();
			CreateMap<Payment,PaymentDAO>().ReverseMap();
			CreateMap<Delivery, DeliveryDAO>().ReverseMap();
			CreateMap<OrderItem, OrderItemDAO>().ReverseMap();
		}
	}
}
