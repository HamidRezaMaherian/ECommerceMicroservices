using Admin.Application.Models.UI;
using Admin.Application.Services;
using System.Linq.Expressions;

namespace Admin.Infrastructure.Services.UI
{
	public class SliderService : IQueryBaseService<Slider>, ICommandBaseService<Slider, Slider>
	{
		public Task AddAsync(Slider entityDTO)
		{
			throw new NotImplementedException();
		}

		public Task DeleteAsync(object id)
		{
			throw new NotImplementedException();
		}

		public Task<Slider> FirstOrDefaultAsync()
		{
			throw new NotImplementedException();
		}

		public Task<TypeDTO> FirstOrDefaultAsync<TypeDTO>() where TypeDTO : class
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Slider>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<TypeDTO>> GetAllAsync<TypeDTO>() where TypeDTO : class
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Slider>> GetAllAsync(Expression<Func<Slider, bool>> condition)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<TypeDTO>> GetAllAsync<TypeDTO>(Expression<Func<Slider, bool>> condition) where TypeDTO : class
		{
			throw new NotImplementedException();
		}

		public Task<Slider> GetByIdAsync(object id)
		{
			throw new NotImplementedException();
		}

		public Task<TypeDTO> GetByIdAsync<TypeDTO>(object id) where TypeDTO : class
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(Slider entityDTO)
		{
			throw new NotImplementedException();
		}
	}
}
