using Order.Application.Services;
using Order.Application.Tools;
using Order.Application.UnitOfWork;
using Order.Domain.Entities;

namespace Order.Infrastructure.Services
{
	public class OrderService : GenericActiveService<Domain.Entities.Order, OrderDTO>, IOrderService
	{
		public OrderService(IUnitOfWork unitOfWork, ICustomMapper mapper) : base(unitOfWork, mapper)
		{
		}

		public void AddDelivery(DeliveryDTO delivery)
		{
			throw new NotImplementedException();
		}

		public void AddItem(string orderid, OrderItemDTO item)
		{
			throw new NotImplementedException();
		}

		public void AddPayment(PaymentDTO payment)
		{
			throw new NotImplementedException();
		}

		public void DeleteItem(string itemId)
		{
			throw new NotImplementedException();
		}

		public void UpdateDelivery(DeliveryDTO delivery)
		{
			throw new NotImplementedException();
		}

		public void UpdateItem(OrderItemDTO item)
		{
			throw new NotImplementedException();
		}

		public void UpdatePayment(PaymentDTO payment)
		{
			throw new NotImplementedException();
		}
	}
}
