using Services.Shared.AppUtils;
using System.Linq.Expressions;

namespace Basket.Application.Repositories
{
	public interface IRepository<T>
		where T : class
	{
		T Get(object id);

		void Delete(object id);
		void Delete(T entity);

		void Add(T entity);

		void Update(T entity);
	}
}
