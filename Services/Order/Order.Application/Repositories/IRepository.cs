using Services.Shared.AppUtils;
using System.Linq.Expressions;

namespace Order.Application.Repositories
{
	public interface IRepository<T>:ICommandRepository<T>,IQueryRepository<T>
		where T : class
	{
	}
	public interface ICommandRepository<T> where T : class
	{
		void Delete(object id);
		void Delete(T entity);

		void Add(T entity);

		void Update(T entity);
	}
	public interface IQueryRepository<T> where T : class
	{
		IEnumerable<T> Get(QueryParams<T> predicate);
		bool Exists(Expression<Func<T, bool>> predicate);
		IEnumerable<T> Get();

		T Get(object id);
	}
}
