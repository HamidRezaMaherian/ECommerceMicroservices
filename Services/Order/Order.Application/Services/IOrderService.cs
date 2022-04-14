using Order.Application.DTOs;

namespace Order.Application.Services;

public interface IOrderService : IBaseService<Domain.Entities.Order, OrderDTO>
{
	void AddItem(string orderid, OrderItemDTO item);
	void UpdateItem(OrderItemDTO item);
	void DeleteItem(string itemId);
	void UpsertDelivery(string orderId,DeliveryDTO delivery);
	void UpsertPayment(string orderId,PaymentDTO payment);
}