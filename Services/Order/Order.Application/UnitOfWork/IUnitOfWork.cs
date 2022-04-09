using Order.Application.Repositories;

namespace Order.Application.UnitOfWork
{
	public interface IUnitOfWork : IDisposable, IAsyncDisposable
	{
		IOrderRepo OrderRepo { get; }
		IDeliveryRepo DeliveryRepo { get; }
		IPaymentRepo PaymentRepo { get; }

		IRepository<T> GetRepo<T>() where T : class;
		void Save();
		Task SaveAsync();
	}
}
