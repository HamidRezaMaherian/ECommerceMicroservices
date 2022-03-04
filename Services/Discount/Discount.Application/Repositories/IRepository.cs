﻿using Discount.Application.Utils;

namespace Discount.Application.Repositories
{
	public interface IRepository<T>
		where T : class
	{
		IEnumerable<T> Get();

		T Get(object id);

		void Delete(object id);
		void Delete(T entity);

		void Add(ref T entity);

		void Update(T entity);
		IEnumerable<T> Get(QueryParams<T> queryParams);
	}
}