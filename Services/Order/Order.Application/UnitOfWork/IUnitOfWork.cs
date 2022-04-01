using Order.Application.Repositories;

namespace Order.Application.UnitOfWork
{
	public interface IUnitOfWork : IDisposable, IAsyncDisposable
	{
		IRepository<T> GetRepo<T>() where T : class;
		void Save();
		Task SaveAsync();
	}
}
