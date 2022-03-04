using AutoMapper;
using Discount.Application.Repositories;
using Discount.Application.UnitOfWork;
using Discount.Infrastructure.Repositories;

namespace Discount.Infrastructure.Persist
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _db;
		private readonly IMapper _mapper;

		public UnitOfWork(ApplicationDbContext db, IMapper mapper)
		{
			_db = db;
			_mapper = mapper;
		}
		#region Product
		private IPercentDiscountRepo _percentDiscountRepo;
		public IPercentDiscountRepo PercentDiscountRepo
		{
			get
			{
				_percentDiscountRepo ??= new PercentDiscountRepo(_db, _mapper);
				return _percentDiscountRepo;
			}
		}
		private IPriceDiscountRepo _priceDiscountRepo;
		public IPriceDiscountRepo PriceDiscountRepo
		{
			get
			{
				_priceDiscountRepo ??= new PriceDiscountRepo(_db, _mapper);
				return _priceDiscountRepo;
			}
		}
		#endregion

		#region Methods
		public IRepository<T> GetRepo<T>()
			where T : class
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
