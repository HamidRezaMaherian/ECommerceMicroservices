using Order.Domain.Entities;
using System.Linq.Expressions;

namespace Order.Application.Repositories
{
	public interface IOrderRepo : IRepository<Domain.Entities.Order>
	{
		bool ItemExists(string orderId, Expression<Func<OrderItem, bool>> exp);
		bool ItemExists(string orderId);
		bool DeliveryExists(string deliveryId, Expression<Func<Delivery, bool>> exp);
		bool DeliveryExists(string deliveryId);
		bool PaymentExists(string paymentId, Expression<Func<Payment, bool>> exp);
		bool PaymentExists(string paymentId);
		void AddItem(string orderId, OrderItem orderItem);
		void UpdateItem(OrderItem orderItem);
		void DeleteItem(string itemId);
		void AddDelivery(string orderId, Delivery delivery);
		void UpdateDelivery(Delivery delivery);
		void DeleteDelivery(string deliveryId);
		void AddPayment(string orderId, Payment payment);
		void UpdatePayment(Payment payment);
		void DeletePayment(string paymentId);
	}
}
