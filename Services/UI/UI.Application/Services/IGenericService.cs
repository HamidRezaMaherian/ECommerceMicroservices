using System.Linq.Expressions;
using UI.Domain.Common;

namespace UI.Application.Services;

public interface IEntityBaseService<T, Tdto> : IBaseActiveService<T, Tdto> where T : IBaseActive
{
}
public interface IBaseActiveService<T, Tdto> : IBaseService<T, Tdto> where T : IBaseActive
{
	IEnumerable<T> GetAllActive();
	IEnumerable<T> GetAllActive(Expression<Func<T, bool>> condition);
}
public interface IBaseService<T, Tdto>
{
	IEnumerable<T> GetAll();
	IEnumerable<T> GetAll(Expression<Func<T, bool>> condition);

	T GetById(object id);

	T FirstOrDefault();

	void Add(Tdto entityDTO);
	void Update(Tdto entityDTO);
	void Delete(object id);
	bool Exists(Expression<Func<T, bool>> condition);
}

