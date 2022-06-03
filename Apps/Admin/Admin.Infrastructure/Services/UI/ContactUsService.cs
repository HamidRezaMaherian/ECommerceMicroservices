using Admin.Application.Models.UI;
using Admin.Application.Services.UI;
using Admin.Infrastructure.APIUtils;

namespace Admin.Infrastructure.Services.UI
{
	public class ContactUsService : IContactUsService
	{
		private readonly HttpClientHelper<GatewayHttpClient> _httpClientHelper;

		public ContactUsService(HttpClientHelper<GatewayHttpClient> httpClientHelper)
		{
			_httpClientHelper = httpClientHelper;
		}

		public Task<ContactUs> FirstOrDefaultAsync()
		{
			throw new NotImplementedException();
		}

		public Task<TypeDTO> FirstOrDefaultAsync<TypeDTO>() where TypeDTO : class
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(ContactUs entityDTO)
		{
			throw new NotImplementedException();
		}
	}
}
