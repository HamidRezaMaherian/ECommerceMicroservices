using Order.Domain.Entities;
using System.Linq.Expressions;

namespace Order.Application.Repositories
{
	public interface IOrderItemRepo : IQueryRepository<OrderItem>
	{
	}
}
