using Admin.Application.Models.UI;
using Admin.Application.Services;
using System.Linq.Expressions;

namespace Admin.Infrastructure.Services.UI
{
	public class FaqService : IQueryBaseService<Faq>, ICommandBaseService<Faq, Faq>
	{
		public Task AddAsync(Faq entityDTO)
		{
			throw new NotImplementedException();
		}

		public Task DeleteAsync(object id)
		{
			throw new NotImplementedException();
		}

		public Task<Faq> FirstOrDefaultAsync()
		{
			throw new NotImplementedException();
		}

		public Task<TypeDTO> FirstOrDefaultAsync<TypeDTO>() where TypeDTO : class
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Faq>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<TypeDTO>> GetAllAsync<TypeDTO>() where TypeDTO : class
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Faq>> GetAllAsync(Expression<Func<Faq, bool>> condition)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<TypeDTO>> GetAllAsync<TypeDTO>(Expression<Func<Faq, bool>> condition) where TypeDTO : class
		{
			throw new NotImplementedException();
		}

		public Task<Faq> GetByIdAsync(object id)
		{
			throw new NotImplementedException();
		}

		public Task<TypeDTO> GetByIdAsync<TypeDTO>(object id) where TypeDTO : class
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(Faq entityDTO)
		{
			throw new NotImplementedException();
		}
	}
}
