using Order.Application.DTOs;
using Order.Application.Services;
using Order.Application.Tools;
using Order.Application.UnitOfWork;

namespace Order.Infrastructure.Services
{
	public class OrderService : GenericActiveService<Domain.Entities.Order, OrderDTO>, IOrderService
	{
		public OrderService(IUnitOfWork unitOfWork, ICustomMapper mapper) : base(unitOfWork, mapper)
		{
		}
		public void AddItem(string orderid, OrderItemDTO item)
		{
			throw new NotImplementedException();
		}

		public void DeleteItem(string itemId)
		{
			throw new NotImplementedException();
		}

		public void UpdateItem(OrderItemDTO item)
		{
			throw new NotImplementedException();
		}

		public void UpsertDelivery(string orderId, DeliveryDTO delivery)
		{
			throw new NotImplementedException();
		}

		public void UpsertPayment(string orderId, PaymentDTO payment)
		{
			throw new NotImplementedException();
		}
	}
}
