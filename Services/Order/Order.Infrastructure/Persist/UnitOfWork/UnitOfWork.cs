using Order.Application.Repositories;
using Order.Application.Tools;
using Order.Application.UnitOfWork;
using Order.Infrastructure.Repositories;

namespace Order.Infrastructure.Persist
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _db;
		private readonly ICustomMapper _mapper;

		public UnitOfWork(ApplicationDbContext db, ICustomMapper mapper)
		{
			_db = db;
			_mapper = mapper;
		}
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
