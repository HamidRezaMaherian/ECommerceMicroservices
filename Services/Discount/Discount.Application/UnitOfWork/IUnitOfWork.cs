using Discount.Application.Repositories;

namespace Discount.Application.UnitOfWork
{
	public interface IUnitOfWork : IDisposable, IAsyncDisposable
	{
		#region Product
		public IPercentDiscountRepo PercentDiscountRepo { get; }
		public IPriceDiscountRepo PriceDiscountRepo { get; }
		#endregion

		IRepository<T> GetRepo<T>() where T : class;
		void Save();
		Task SaveAsync();
	}
}
