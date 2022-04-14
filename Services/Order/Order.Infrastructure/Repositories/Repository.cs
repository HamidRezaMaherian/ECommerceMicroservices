using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Order.Application.Exceptions;
using Order.Application.Repositories;
using Order.Application.Tools;
using Order.Infrastructure.Persist;
using Services.Shared.AppUtils;
using System.Linq.Expressions;

namespace Order.Infrastructure.Repositories
{
	public abstract class Repository<T, TDAO> : IRepository<T>
		where T : class
		where TDAO : class
	{
		protected readonly ApplicationDbContext _db;
		protected readonly DbSet<TDAO> _dbSet;
		protected readonly ICustomMapper _mapper;
		public Repository(ApplicationDbContext db, ICustomMapper mapper)
		{
			_db = db;
			_dbSet = _db.Set<TDAO>();
			_mapper = mapper;
		}
		public virtual void Add(T entity)
		{
			EntityEntry result;
			result = _dbSet.Add(_mapper.Map<TDAO>(entity));
			try
			{
				_db.SaveChanges();
			}
			catch (Exception e)
			{
				throw new InsertOperationException(e.Message, e.InnerException);
			}
			finally
			{
				DetachEntity(result.Entity);
			}
			_mapper.Map(result.Entity, entity);
		}
		public virtual void Delete(object id)
		{
			try
			{
				var entity = Get(id);
				_dbSet.Remove(_mapper.Map<TDAO>(entity));
			}
			catch (Exception e)
			{
				throw new DeleteOperationException(e.Message, e.InnerException);
			}
		}
		public bool Exists(Expression<Func<T, bool>> predicate)
		{
			try
			{
				return _dbSet.Any(ExpressionHelper.Convert<T, TDAO>(predicate));
			}
			catch (Exception e)
			{
				throw new ReadOperationException(e.Message, e.InnerException);
			}
		}

		public void Delete(T entity)
		{
			try
			{
				var entityDAO = _mapper.Map<TDAO>(entity);
				DetachEntity(entityDAO);
				_dbSet.Remove(entityDAO);
			}
			catch (Exception e)
			{
				throw new DeleteOperationException(e.Message, e.InnerException);
			}
		}

		public virtual IEnumerable<T> Get(QueryParams<T> queryParams)
		{
			try
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
			catch (Exception e)
			{
				throw new ReadOperationException(e.Message, e.InnerException);
			}
		}
		public virtual IEnumerable<T> Get()
		{
			try
			{
				return _mapper.Map<IEnumerable<T>>(_dbSet.AsNoTracking().ToList());
			}
			catch (Exception e)
			{
				throw new ReadOperationException(e.Message, e.InnerException);
			}
		}

		public virtual T Get(object id)
		{
			ArgumentNullException.ThrowIfNull(id);
			try
			{
				var entity = _dbSet.Find(id);
				DetachEntity(entity);
				return _mapper.Map<T>(entity);
			}
			catch (Exception e)
			{
				throw new ReadOperationException(e.Message, e.InnerException);
			}
		}

		public virtual void Update(T entity)
		{
			ArgumentNullException.ThrowIfNull(entity);
			var entityDAO = _mapper.Map<TDAO>(entity);
			try
			{
				_db.Entry(entityDAO).State = EntityState.Modified;
				_db.SaveChanges();
			}
			catch (Exception e)
			{
				throw new UpdateOperationException(e.Message, e.InnerException);
			}
			finally
			{
				DetachEntity(entityDAO);
			}
		}
		protected void DetachEntity(object entity)
		{
			if (entity != null)
				_db.Entry(entity).State = EntityState.Detached;
		}
	}
}
