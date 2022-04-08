using Order.Application.Tools;
using Order.Domain.Entities;
using Order.Infrastructure.Persist;
using Order.Infrastructure.Persist.DAOs;

namespace Order.Infrastructure.Repositories
{
	public class OrderItemRepo : Repository<OrderItem, OrderItemDAO>
	{
		public OrderItemRepo(ApplicationDbContext db, ICustomMapper mapper) : base(db, mapper)
		{
		}
	}
}
