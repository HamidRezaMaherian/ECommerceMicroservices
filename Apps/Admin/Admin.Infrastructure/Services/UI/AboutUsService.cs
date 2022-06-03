using Admin.Application.Models.UI;
using Admin.Application.Services.UI;
using Admin.Infrastructure.APIUtils;

namespace Admin.Infrastructure.Services.UI
{
	public class AboutUsService : IAboutUsService
	{
		private readonly HttpClientHelper<GatewayHttpClient> _httpClientHelper;

		public AboutUsService(HttpClientHelper<GatewayHttpClient> httpClientHelper)
		{
			_httpClientHelper = httpClientHelper;
		}

		public Task<AboutUs> FirstOrDefaultAsync()
		{
			throw new NotImplementedException();
		}

		public Task<TypeDTO> FirstOrDefaultAsync<TypeDTO>() where TypeDTO : class
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(AboutUs entityDTO)
		{
			throw new NotImplementedException();
		}
	}
}
