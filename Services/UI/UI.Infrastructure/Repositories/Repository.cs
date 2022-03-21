using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Services.Shared.AppUtils;
using Services.Shared.Common;
using Services.Shared.Contracts;
using System.Linq.Expressions;
using UI.Application.Exceptions;
using UI.Infrastructure.Persist;
using UI.Infrastructure.Persist.DAOs;

namespace UI.Infrastructure.Repositories
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
			_dbCollection = _db.DataBase.GetCollection<TDAO>(nameof(T));
			_mapper = mapper;
		}
		public virtual void Add(T entity)
		{
			try
			{
			_dbCollection.InsertOne(_mapper.Map<TDAO>(entity));
			}
			catch (Exception e)
			{
				throw new InsertOperationException(e.Message,e.InnerException);
			}
		}
		public virtual void Delete(object id)
		{
			var entity = Get(id);
			if (entity == null)
				return;
			_dbCollection.DeleteOne(i => i.Id == entity.Id);
		}

		public void Delete(T entity)
		{
			_dbCollection.DeleteOne(i => i.Id == entity.Id);
		}

		public bool Exists(Expression<Func<T, bool>> predicate)
		{
			IMongoQueryable<TDAO> query = _dbCollection.AsQueryable();
			return query.Any(
				ExpressionHelper.Convert<T, TDAO>(predicate)
				);
		}

		public virtual IEnumerable<T> Get(QueryParams<T> queryParams)
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
		public virtual IEnumerable<T> Get()
		{
			return _mapper.Map<IEnumerable<T>>(_dbCollection.AsQueryable().ToList());
		}

		public virtual T Get(object id)
		{
			ArgumentNullException.ThrowIfNull(id);
			return _mapper.Map<T>(_dbCollection.AsQueryable().FirstOrDefault(i => i.Id == id));
		}

		public virtual void Update(T entity)
		{
			ArgumentNullException.ThrowIfNull(entity);
			_dbCollection.ReplaceOne(i => i.Id == entity.Id, _mapper.Map<TDAO>(entity));
		}
	}
}
