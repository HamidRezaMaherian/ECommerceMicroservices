using Services.Shared.Contracts;

namespace UI.Application.UnitOfWork
{
	public interface IUnitOfWork : IDisposable, IAsyncDisposable
	{

		IRepository<T> GetRepo<T>() where T : class;
	}
}
