using Order.Domain.Entities;

namespace Order.Application.Services;

public interface IOrderService : IBaseService<Domain.Entities.Order, OrderDTO>
{
	void AddItem(string orderid, OrderItemDTO item);
	void UpdateItem(OrderItemDTO item);
	void DeleteItem(string itemId);
	void AddDelivery(DeliveryDTO delivery);
	void UpdateDelivery(DeliveryDTO delivery);
	void AddPayment(PaymentDTO payment);
	void UpdatePayment(PaymentDTO payment);
}