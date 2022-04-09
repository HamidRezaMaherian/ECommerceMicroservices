using Microsoft.EntityFrameworkCore;
using Order.Application.Exceptions;
using Order.Application.Repositories;
using Order.Application.Tools;
using Order.Domain.Entities;
using Order.Infrastructure.Persist;
using Order.Infrastructure.Persist.DAOs;
using Services.Shared.AppUtils;
using System.Linq.Expressions;

namespace Order.Infrastructure.Repositories
{
	public class OrderItemRepo : IOrderItemRepo
	{
		protected readonly ApplicationDbContext _db;
		protected readonly DbSet<OrderItemDAO> _dbSet;
		private readonly ICustomMapper _mapper;
		public OrderItemRepo(ApplicationDbContext db, ICustomMapper mapper)
		{
			_db = db;
			_dbSet = _db.Set<OrderItemDAO>();
			_mapper = mapper;
		}
		public bool Exists(Expression<Func<OrderItem, bool>> predicate)
		{
			try
			{
				return _dbSet.Any(ExpressionHelper.Convert<OrderItem, OrderItemDAO>(predicate));
			}
			catch (Exception e)
			{
				throw new ReadOperationException(e.Message, e.InnerException);
			}
		}
		public virtual IEnumerable<OrderItem> Get(QueryParams<OrderItem> queryParams)
		{
			try
			{
				IQueryable<OrderItemDAO> query = _dbSet.AsNoTracking();
				query = query.Where(
					ExpressionHelper.Convert<OrderItem, OrderItemDAO>(queryParams.Expression)
					);

				//foreach (var includeProperty in queryParams.IncludeProperties.Split
				//			(',', StringSplitOptions.RemoveEmptyEntries))
				//	query = query.Include(includeProperty);

				//if (queryParams.OrderBy != null)
				//	queryParams.OrderBy(query);

				query = queryParams.Skip != 0 ? query.Skip(queryParams.Skip) : query;
				query = queryParams.Take != 0 ? query.Take(queryParams.Take) : query;
				return _mapper.Map<IEnumerable<OrderItem>>(query.ToList());
			}
			catch (Exception e)
			{
				throw new ReadOperationException(e.Message, e.InnerException);
			}
		}
		public virtual IEnumerable<OrderItem> Get()
		{
			try
			{
				return _mapper.Map<IEnumerable<OrderItem>>(_dbSet.AsNoTracking().ToList());
			}
			catch (Exception e)
			{
				throw new ReadOperationException(e.Message, e.InnerException);
			}
		}

		public virtual OrderItem Get(object id)
		{
			ArgumentNullException.ThrowIfNull(id);
			try
			{
				var entity = _dbSet.Find(id);
				DetachEntity(entity);
				return _mapper.Map<OrderItem>(entity);
			}
			catch (Exception e)
			{
				throw new ReadOperationException(e.Message, e.InnerException);
			}
		}
		private void DetachEntity(object entity)
		{
			if (entity != null)
				_db.Entry(entity).State = EntityState.Detached;
		}

	}
}
