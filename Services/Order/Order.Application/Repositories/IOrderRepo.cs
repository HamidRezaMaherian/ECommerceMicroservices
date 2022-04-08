using Order.Domain.Entities;

namespace Order.Application.Repositories
{
	public interface IOrderRepo : IRepository<Domain.Entities.Order>
	{
	}
}
