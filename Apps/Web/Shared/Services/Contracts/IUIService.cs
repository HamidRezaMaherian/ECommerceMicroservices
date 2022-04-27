using WebApp.Shared.Models.UI;

namespace WebApp.Shared.Services.Contracts
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
