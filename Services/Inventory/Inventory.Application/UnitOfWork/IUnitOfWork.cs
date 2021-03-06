using Inventory.Application.Repositories;

namespace Inventory.Application.UnitOfWork
{
	public interface IUnitOfWork : IDisposable, IAsyncDisposable
	{
		public IStockRepo StockRepo { get; }
		public IStoreRepo StoreRepo { get; }

		IRepository<T> GetRepo<T>() where T : class;
	}
}
