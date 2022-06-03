using Admin.Application.Models.UI;
using Admin.Application.Services;
using System.Linq.Expressions;

namespace Admin.Infrastructure.Services.UI
{
	public class SocialMediaService : IQueryBaseService<SocialMedia>, ICommandBaseService<SocialMedia, SocialMedia>
	{
		public Task AddAsync(SocialMedia entityDTO)
		{
			throw new NotImplementedException();
		}

		public Task DeleteAsync(object id)
		{
			throw new NotImplementedException();
		}

		public Task<SocialMedia> FirstOrDefaultAsync()
		{
			throw new NotImplementedException();
		}

		public Task<TypeDTO> FirstOrDefaultAsync<TypeDTO>() where TypeDTO : class
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<SocialMedia>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<TypeDTO>> GetAllAsync<TypeDTO>() where TypeDTO : class
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<SocialMedia>> GetAllAsync(Expression<Func<SocialMedia, bool>> condition)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<TypeDTO>> GetAllAsync<TypeDTO>(Expression<Func<SocialMedia, bool>> condition) where TypeDTO : class
		{
			throw new NotImplementedException();
		}

		public Task<SocialMedia> GetByIdAsync(object id)
		{
			throw new NotImplementedException();
		}

		public Task<TypeDTO> GetByIdAsync<TypeDTO>(object id) where TypeDTO : class
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(SocialMedia entityDTO)
		{
			throw new NotImplementedException();
		}
	}
}
