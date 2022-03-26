using Product.Domain.Common;
using System.Linq.Expressions;

namespace Product.Application.Services;
public interface IEntityBaseService<T, Tdto> : IBaseActiveService<T, Tdto> where T : IBaseActive
{
}
public interface IBaseActiveService<T, Tdto> : IBaseService<T, Tdto> where T : IBaseActive
{
	IEnumerable<T> GetAllActive();
	IEnumerable<TypeDTO> GetAllActive<TypeDTO>() where TypeDTO : class;
	IEnumerable<T> GetAllActive(Expression<Func<T, bool>> condition);
	IEnumerable<TypeDTO> GetAllActive<TypeDTO>(Expression<Func<T, bool>> condition) where TypeDTO : class;
}
public interface IBaseService<T, Tdto>
{
	IEnumerable<T> GetAll();
	IEnumerable<TypeDTO> GetAll<TypeDTO>() where TypeDTO : class;
	IEnumerable<T> GetAll(Expression<Func<T, bool>> condition);
	IEnumerable<TypeDTO> GetAll<TypeDTO>(Expression<Func<T, bool>> condition) where TypeDTO : class;

	T GetById(object id);
	TypeDTO GetById<TypeDTO>(object id) where TypeDTO : class;

	T FirstOrDefault();
	TypeDTO FirstOrDefault<TypeDTO>() where TypeDTO : class;

	void Add(Tdto entityDTO);
	void Update(Tdto entityDTO);
	void Delete(object id);
	bool Exists(Expression<Func<T, bool>> condition);
}

