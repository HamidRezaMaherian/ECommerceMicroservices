using Order.Domain.Entities;
using System.Linq.Expressions;

namespace Order.Application.Repositories
{
	public interface IOrderRepo : IRepository<Domain.Entities.Order>
	{
		bool ItemExists(string orderId, Expression<Func<OrderItem, bool>> exp);
		bool ItemExists(string orderId);
		bool AddItem(string orderId, OrderItem orderItem);
		bool UpdateItem(OrderItem orderItem);
		bool DeleteItem(string itemId);
	}
}
