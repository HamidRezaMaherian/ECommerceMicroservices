using Inventory.Domain.Entities;
using Services.Shared.Contracts;

namespace Inventory.Application.Repositories
{
	public interface IStockRepo : IRepository<Stock>
	{
	}
}
