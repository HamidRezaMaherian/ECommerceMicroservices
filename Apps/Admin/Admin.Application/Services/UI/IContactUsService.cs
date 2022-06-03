using Admin.Application.Models.UI;
using System.Linq.Expressions;

namespace Admin.Application.Services.UI
{
	public interface IContactUsService
	{
		Task<ContactUs> FirstOrDefaultAsync();
		Task<TypeDTO> FirstOrDefaultAsync<TypeDTO>() where TypeDTO : class;
		Task UpdateAsync(ContactUs entityDTO);
	}
}
