using Inventory.Application.Exceptions;
using Inventory.Application.Repositories;
using Inventory.Application.Tools;
using Inventory.Domain.Common;
using Inventory.Infrastructure.Persist;
using Inventory.Infrastructure.Persist.DAOs;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Services.Shared.AppUtils;
using System.Linq.Expressions;

namespace Inventory.Infrastructure.Repositories
{
	public abstract class Repository<T, TDAO> : IRepository<T>
		where T : EntityPrimaryBase<string>
		where TDAO : EntityPrimaryBaseDAO<string>
	{
		protected readonly ApplicationDbContext _db;
		protected readonly IMongoCollection<TDAO> _dbCollection;
		private readonly ICustomMapper _mapper;
		public Repository(ApplicationDbContext db, ICustomMapper mapper)
		{
			_db = db;
			_dbCollection = _db.DataBase.GetCollection<TDAO>(typeof(T).Name);
			_mapper = mapper;
		}
		public virtual void Add(T entity)
		{
			try
			{
				entity.Id = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 24);
				_dbCollection.InsertOne(_mapper.Map<TDAO>(entity));
			}
			catch (Exception e)
			{
				throw new InsertOperationException(e.Message, e.InnerException);
			}
		}
		public virtual void Delete(object id)
		{
			try
			{
				_dbCollection.DeleteOne(i => i.Id == id.ToString());
			}
			catch (Exception e)
			{
				throw new DeleteOperationException(e.Message, e.InnerException);
			}
		}

		public void Delete(T entity)
		{
			try
			{
				_dbCollection.DeleteOne(i => i.Id == entity.Id);
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
				IMongoQueryable<TDAO> query = _dbCollection.AsQueryable();
				return query.Any(
					ExpressionHelper.Convert<T, TDAO>(predicate)
					);
			}
			catch (Exception e)
			{
				throw new ReadOperationException(e.Message, e.InnerException);
			}
		}

		public virtual IEnumerable<T> Get(QueryParams<T> queryParams)
		{
			try
			{
				IMongoQueryable<TDAO> query = _dbCollection.AsQueryable();
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
				return _mapper.Map<IEnumerable<T>>(_dbCollection.AsQueryable().ToList());
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
				return _mapper.Map<T>(_dbCollection.AsQueryable().FirstOrDefault(i => i.Id == id.ToString()));
			}
			catch (Exception e)
			{
				throw new ReadOperationException(e.Message, e.InnerException);
			}
		}

		public virtual void Update(T entity)
		{
			ArgumentNullException.ThrowIfNull(entity);
			try
			{
				_dbCollection.ReplaceOne(i => i.Id == entity.Id, _mapper.Map<TDAO>(entity));
			}
			catch (Exception e)
			{
				throw new UpdateOperationException(e.Message, e.InnerException);
			}
		}
	}
}
