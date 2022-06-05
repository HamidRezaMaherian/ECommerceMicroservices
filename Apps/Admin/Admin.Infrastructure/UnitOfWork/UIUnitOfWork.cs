using Admin.Application.Services.UI;
using Admin.Application.UnitOfWork.UI;
using Microsoft.Extensions.DependencyInjection;

namespace Admin.Infrastructure.UnitOfWork
{
	public class UIUnitOfWork : IUIUnitOfWork
	{
		private readonly IServiceProvider _serviceProvider;

		public UIUnitOfWork(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}
		public IFaqCategoryService FaqCategory => _serviceProvider.GetService<IFaqCategoryService>();

		public IFaqService Faq => _serviceProvider.GetService<IFaqService>();

		public ISocialMediaService SocialMedia => _serviceProvider.GetService<ISocialMediaService>();

		public ISliderService Slider => _serviceProvider.GetService<ISliderService>();

		public IContactUsService ContactUs => _serviceProvider.GetService<IContactUsService>();

		public IAboutUsService AboutUs => _serviceProvider.GetService<IAboutUsService>();
	}
}
