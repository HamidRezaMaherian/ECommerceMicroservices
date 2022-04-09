using Order.Application.Repositories;
using Order.Application.Tools;
using Order.Domain.Entities;
using Order.Infrastructure.Persist;
using Order.Infrastructure.Persist.DAOs;
using Services.Shared.AppUtils;
using System.Linq.Expressions;

namespace Order.Infrastructure.Repositories
{
	public class OrderRepo : Repository<Domain.Entities.Order, OrderDAO>, IOrderRepo
	{
		public OrderRepo(ApplicationDbContext db, ICustomMapper mapper) : base(db, mapper)
		{
		}

		public bool ItemExists(string orderId, Expression<Func<OrderItem, bool>> exp)
		{
			return _db.OrderItems.Where(i => i.OrderId == orderId).Any(
				ExpressionHelper.Convert<OrderItem,OrderItemDAO>(exp)
				);
		}

		public bool ItemExists(string orderId)
		{
			return _db.OrderItems.Any(i => i.OrderId == orderId);
		}
	}
}
