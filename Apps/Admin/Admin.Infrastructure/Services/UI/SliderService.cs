using Admin.Application.DTOs.UI;
using Admin.Application.Models.UI;
using Admin.Application.Services.UI;
using Admin.Infrastructure.APIUtils;
using System.Linq.Expressions;

namespace Admin.Infrastructure.Services.UI
{
	public class SliderService : ISliderService
	{
		private readonly HttpClientHelper<GatewayHttpClient> _httpClientHelper;

		public SliderService(HttpClientHelper<GatewayHttpClient> httpClientHelper)
		{
			_httpClientHelper = httpClientHelper;
		}

		public Task AddAsync(SliderDTO entityDTO)
		{
			return _httpClientHelper.PostAsync("ui/slider/create", entityDTO);
		}

		public Task DeleteAsync(object id)
		{
			return _httpClientHelper.DeleteAsync($"ui/slider/delete/{id}");
		}

		public async Task<Slider> FirstOrDefaultAsync()
		{
			return (await _httpClientHelper.GetAsync<IEnumerable<Slider>>("ui/slider/getall")).FirstOrDefault();
		}

		public async Task<TypeDTO> FirstOrDefaultAsync<TypeDTO>() where TypeDTO : class
		{
			return (await _httpClientHelper.GetAsync<IEnumerable<TypeDTO>>("ui/slider/getall")).FirstOrDefault();
		}

		public async Task<IEnumerable<Slider>> GetAllAsync()
		{
			return await _httpClientHelper.GetAsync<IEnumerable<Slider>>("ui/slider/getall");
		}

		public Task<IEnumerable<TypeDTO>> GetAllAsync<TypeDTO>() where TypeDTO : class
		{
			return _httpClientHelper.GetAsync<IEnumerable<TypeDTO>>("ui/slider/getall");
		}

		public async Task<IEnumerable<Slider>> GetAllAsync(Expression<Func<Slider, bool>> condition)
		{
			return (await _httpClientHelper.GetAsync<IEnumerable<Slider>>("ui/slider/getall")).AsQueryable().Where(condition);
		}

		public async Task<IEnumerable<TypeDTO>> GetAllAsync<TypeDTO>(Expression<Func<TypeDTO, bool>> condition) where TypeDTO : class
		{
			return (await _httpClientHelper.GetAsync<IEnumerable<TypeDTO>>("ui/slider/getall")).AsQueryable().Where(condition);
		}

		public Task<Slider> GetByIdAsync(object id)
		{
			return _httpClientHelper.GetAsync<Slider>($"ui/slider/get/{id}");
		}

		public Task<TypeDTO> GetByIdAsync<TypeDTO>(object id) where TypeDTO : class
		{
			return _httpClientHelper.GetAsync<TypeDTO>($"ui/slider/get/{id}");
		}

		public Task UpdateAsync(SliderDTO entityDTO)
		{
			return _httpClientHelper.PostAsync($"ui/slider/update",entityDTO);
		}
	}
}
