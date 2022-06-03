using Admin.Application.Models.UI;

namespace Admin.Application.Services.UI
{
	public interface IAboutUsService
	{
		Task<AboutUs> FirstOrDefaultAsync();
		Task<TypeDTO> FirstOrDefaultAsync<TypeDTO>() where TypeDTO : class;
		Task UpdateAsync(AboutUs entityDTO);
	}
}
