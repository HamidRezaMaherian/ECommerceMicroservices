using Microsoft.EntityFrameworkCore;
using Product.Application.Repositories;
using Product.Infrastructure.Persist;

namespace Product.Infrastructure.Repositories
{
	public abstract class Repository<T, TDAO> : IRepository<T>
		where T : class
		where TDAO : class
	{
		protected readonly ApplicationDbContext _db;
		protected readonly DbSet<TDAO> _dbSet;

		public Repository(ApplicationDbContext db)
		{
			_db = db;
			_dbSet = _db.Set<TDAO>();
		}
		public virtual void Add(T entity)
		{
			_dbSet.Add(entity as TDAO);
		}
		public virtual void Delete(object id)
		{
			var entity = Get(id);
			if (entity == null)
				return;
			_dbSet.Remove(entity as TDAO);
		}

		public void Delete(T entity)
		{
			_dbSet.Remove(entity as TDAO);
		}

		public virtual IQueryable<T> Get()
		{
			return _dbSet as IQueryable<T>;
		}

		public virtual T Get(object id)
		{
			return _dbSet.Find(id) as T;
		}

		public virtual void Update(T entity)
		{
			_dbSet.Update(entity as TDAO);
		}
	}
}
