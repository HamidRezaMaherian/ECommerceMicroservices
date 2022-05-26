using Admin.Application.Models.UI;

namespace Admin.Application.Services
{
	public interface IUIService
	{
		IEnumerable<Slider> GetSliders();
		IEnumerable<FaqCategory> GetFaqs();
		IEnumerable<SocialMedia> GetSocialMedias();
		AboutUs GetAboutUs();
		ContactUs GetContactUs();
	}
}
