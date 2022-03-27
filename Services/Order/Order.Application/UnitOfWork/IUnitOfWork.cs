using Order.Application.Repositories;

namespace Order.Application.UnitOfWork
{
	public interface IUnitOfWork : IDisposable, IAsyncDisposable
	{
		#region Product
		public IProductRepo ProductRepo { get; }
		public IProductCategoryRepo ProductCategoryRepo { get; }
		public IProductImageRepo ProductImageRepo { get; }
		public IPropertyRepo PropertyRepo { get; }
		public IBrandRepo BrandRepo { get; }
		public ICategoryPropertyRepo CategoryPropertyRepo { get; }
		#endregion

		IRepository<T> GetRepo<T>() where T : class;
		void Save();
		Task SaveAsync();
	}
}
