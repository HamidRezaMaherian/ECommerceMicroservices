using System.Linq.Expressions;

namespace Admin.Application.Services;

public interface ICommandBaseService<T, Tdto>
{
	Task AddAsync(Tdto entityDTO);
	Task UpdateAsync(Tdto entityDTO);
	Task DeleteAsync(object id);
}
public interface IQueryBaseService<T>
{
	Task<IEnumerable<T>> GetAllAsync();
	Task<IEnumerable<TypeDTO>> GetAllAsync<TypeDTO>() where TypeDTO : class;
	Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> condition);
	Task<IEnumerable<TypeDTO>> GetAllAsync<TypeDTO>(Expression<Func<T, bool>> condition) where TypeDTO : class;

	Task<T> GetByIdAsync(object id);
	Task<TypeDTO> GetByIdAsync<TypeDTO>(object id) where TypeDTO : class;

	Task<T> FirstOrDefaultAsync();
	Task<TypeDTO> FirstOrDefaultAsync<TypeDTO>() where TypeDTO : class;

}
