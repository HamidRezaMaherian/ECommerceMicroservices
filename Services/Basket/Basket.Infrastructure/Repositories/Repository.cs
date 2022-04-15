using Basket.Application.Exceptions;
using Basket.Application.Repositories;
using Basket.Domain.Common;
using Microsoft.Extensions.Caching.Distributed;
using Services.Shared.AppUtils;

namespace Basket.Infrastructure.Repositories
{
	public abstract class Repository<T> : IRepository<T>
		where T : EntityPrimaryBase<string>
	{
		private readonly IDistributedCache _cache;
		public Repository(IDistributedCache cache)
		{
			_cache = cache;
		}
		public virtual void Add(T entity)
		{
			try
			{
				_cache.SetString(entity.Id, JsonHelper.Stringify(entity));
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
				_cache.Remove(id.ToString());
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
				_cache.Remove(entity.Id);
			}
			catch (Exception e)
			{
				throw new DeleteOperationException(e.Message, e.InnerException);
			}
		}

		public virtual T Get(object id)
		{
			try
			{
				return JsonHelper.Parse<T>(_cache.GetString(id.ToString()));
			}
			catch (Exception e)
			{
				throw new ReadOperationException(e.Message, e.InnerException);
			}
		}

		public virtual void Update(T entity)
		{
			try
			{
				_cache.Remove(entity.Id);
				_cache.SetString(entity.Id, JsonHelper.Stringify(entity));
			}
			catch (Exception e)
			{
				throw new UpdateOperationException(e.Message, e.InnerException);
			}

		}
	}
}
