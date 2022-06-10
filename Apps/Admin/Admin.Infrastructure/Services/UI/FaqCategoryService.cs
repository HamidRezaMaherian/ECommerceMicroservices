using Admin.Application.Models.UI;
using Admin.Application.Services;
using Admin.Application.Services.UI;
using System.Linq.Expressions;

namespace Admin.Infrastructure.Services.UI
{
	public class FaqCategoryService : IFaqCategoryService
	{
		public Task AddAsync(FaqCategory entityDTO)
		{
			throw new NotImplementedException();
		}

		public Task DeleteAsync(object id)
		{
			throw new NotImplementedException();
		}

		public Task<FaqCategory> FirstOrDefaultAsync()
		{
			throw new NotImplementedException();
		}

		public Task<TypeDTO> FirstOrDefaultAsync<TypeDTO>() where TypeDTO : class
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<FaqCategory>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<TypeDTO>> GetAllAsync<TypeDTO>() where TypeDTO : class
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<FaqCategory>> GetAllAsync(Expression<Func<FaqCategory, bool>> condition)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<TypeDTO>> GetAllAsync<TypeDTO>(Expression<Func<TypeDTO, bool>> condition) where TypeDTO : class
		{
			throw new NotImplementedException();
		}

		public Task<FaqCategory> GetByIdAsync(object id)
		{
			throw new NotImplementedException();
		}

		public Task<TypeDTO> GetByIdAsync<TypeDTO>(object id) where TypeDTO : class
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(FaqCategory entityDTO)
		{
			throw new NotImplementedException();
		}
	}
}
