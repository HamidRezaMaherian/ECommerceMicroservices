using AutoMapper;
using Discount.Infrastructure.Persist;
using Microsoft.EntityFrameworkCore;
using Services.Shared.AppUtils;
using Services.Shared.Contracts;

namespace Discount.Infrastructure.Repositories
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
			_mapper.Map(result.Entity, entity);
		}
		public virtual void Delete(object id)
		{
			var entity = Get(id);
			if (entity == null)
				return;
			_dbSet.Remove(_mapper.Map<TDAO>(entity));
		}

		public void Delete(T entity)
		{
			_dbSet.Remove(_mapper.Map<TDAO>(entity));
		}

		public virtual IEnumerable<T> Get(QueryParams<T> queryParams)
		{
			IQueryable<TDAO> query = _dbSet;
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
			return _mapper.Map<IEnumerable<T>>(_dbSet.ToList());
		}

		public virtual T Get(object id)
		{
			ArgumentNullException.ThrowIfNull(id);
			return _mapper.Map<T>(_dbSet.Find(id));
		}

		public virtual void Update(T entity)
		{
			ArgumentNullException.ThrowIfNull(entity);
			_dbSet.Update(_mapper.Map<TDAO>(entity));
		}
	}
}
