using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Product.Infrastructure.Persist;
using Services.Shared.AppUtils;
using Services.Shared.Contracts;
using System.Linq.Expressions;

namespace Product.Infrastructure.Repositories
{
	public abstract class Repository<T, TDAO> : IRepository<T>
		where T : class
		where TDAO : class
	{
		protected readonly ApplicationDbContext _db;
		protected readonly DbSet<TDAO> _dbSet;
		private readonly IMapper _mapper;
		public Repository(ApplicationDbContext db, IMapper mapper)
		{
			_db = db;
			_dbSet = _db.Set<TDAO>();
			_mapper = mapper;
		}
		public virtual void Add(T entity)
		{
			var result = _dbSet.Add(_mapper.Map<TDAO>(entity));
			_db.SaveChanges();
			DetachEntity(result.Entity);
			_mapper.Map(result.Entity, entity);
		}
		public virtual void Delete(object id)
		{
			var entity = Get(id);
			if (entity == null)
				return;
			_dbSet.Remove(_mapper.Map<TDAO>(entity));
		}
		public bool Exists(Expression<Func<T, bool>> predicate)
		{
			return _dbSet.Any(ExpressionHelper.Convert<T, TDAO>(predicate));
		}

		public void Delete(T entity)
		{
			var entityDAO = _mapper.Map<TDAO>(entity);
			DetachEntity(entityDAO);
			_dbSet.Remove(entityDAO);
		}

		public virtual IEnumerable<T> Get(QueryParams<T> queryParams)
		{
			IQueryable<TDAO> query = _dbSet.AsNoTracking();
			query = query.Where(
				ExpressionHelper.Convert<T, TDAO>(queryParams.Expression)
				);

			//foreach (var includeProperty in queryParams.IncludeProperties.Split
			//			(',', StringSplitOptions.RemoveEmptyEntries))
			//	query = query.Include(includeProperty);

			//if (queryParams.OrderBy != null)
			//	queryParams.OrderBy(query);

			query = queryParams.Skip != 0 ? query.Skip(queryParams.Skip) : query;
			query = queryParams.Take != 0 ? query.Take(queryParams.Take) : query;
			return _mapper.Map<IEnumerable<T>>(query.ToList());
		}
		public virtual IEnumerable<T> Get()
		{
			return _mapper.Map<IEnumerable<T>>(_dbSet.AsNoTracking().ToList());
		}

		public virtual T Get(object id)
		{
			ArgumentNullException.ThrowIfNull(id);
			var entity = _dbSet.Find(id);
			DetachEntity(entity);
			return _mapper.Map<T>(entity);
		}

		public virtual void Update(T entity)
		{
			ArgumentNullException.ThrowIfNull(entity);
			var entityDAO = _mapper.Map<TDAO>(entity);
			DetachEntity(entityDAO);
			_dbSet.Update(entityDAO);
		}
		private void DetachEntity(object entity)
		{
			if (entity != null)
				_db.Entry(entity).State = EntityState.Detached;
		}
	}
}
