using Admin.Application.Services.UI;

namespace Admin.Application.UnitOfWork.UI
{
	public interface IUIUnitOfWork
	{
		IFaqCategoryService FaqCategory { get; }
		IFaqService Faq { get; }
		ISocialMediaService SocialMedia { get; }
		ISliderService Slider { get; }
		IContactUsService ContactUs { get; }
		IAboutUsService AboutUs { get; }
	}
}
