using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Product.Application.Repositories
{
	public interface IRepository<T> where T : class
	{
		IQueryable<T> Get();

		T Get(object id);

		void Delete(object id);
		void Delete(T entity);

		void Add(T entity);

		void Update(T entity);

	}
}
