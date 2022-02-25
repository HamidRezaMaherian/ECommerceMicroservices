using Product.Application.Repositories;
using Product.Application.UnitOfWork;
using Product.Infrastructure.Repositories;

namespace Product.Infrastructure.Persist
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _db;
		public UnitOfWork(ApplicationDbContext db)
		{
			_db = db;
		}
		#region Product
		private IProductRepo _productRepo;
		public IProductRepo ProductRepo
		{
			get
			{
				_productRepo ??= new ProductRepo(_db);
				return _productRepo;
			}
		}
		private IBrandRepo _brandRepo;
		public IBrandRepo BrandRepo
		{
			get
			{
				_brandRepo ??= new BrandRepo(_db);
				return _brandRepo;
			}
		}
		private IProductCategoryRepo _productCategoryRepo;
		public IProductCategoryRepo ProductCategoryRepo
		{
			get
			{
				_productCategoryRepo ??= new ProductCategoryRepo(_db);
				return _productCategoryRepo;
			}
		}
		private IProductImageRepo _productImageRepo;
		public IProductImageRepo ProductImageRepo
		{
			get
			{
				_productImageRepo ??= new ProductImageRepo(_db);
				return _productImageRepo;
			}
		}
		private IPropertyRepo _propertyRepo;
		public IPropertyRepo PropertyRepo
		{
			get
			{
				_propertyRepo ??= new PropertyRepo(_db);
				return _propertyRepo;
			}
		}
		private ICategoryPropertyRepo _categoryPropertyRepo;
		public ICategoryPropertyRepo CategoryPropertyRepo
		{
			get
			{
				_categoryPropertyRepo ??= new CategoryPropertyRepo(_db);
				return _categoryPropertyRepo;
			}
		}
		#endregion

		#region Methods
		public IRepository<T> GetRepo<T>() where T : class
		{
			var props = GetType().GetProperties();
			var res = props.FirstOrDefault(i => i.PropertyType.GetInterfaces().Contains(typeof(IRepository<T>)))?.GetValue(this, null);
			return res as IRepository<T>;
		}
		public void Dispose()
		{
			_db.Dispose();
		}

		public async ValueTask DisposeAsync()
		{
			await _db.DisposeAsync();
		}

		public void Save()
		{
			_db.SaveChanges();
		}

		public async Task SaveAsync()
		{
			await _db.SaveChangesAsync();
		}

		#endregion
	}
}
