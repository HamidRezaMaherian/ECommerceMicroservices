using UI.Application.Repositories;

namespace UI.Application.UnitOfWork
{
	public interface IUnitOfWork : IDisposable, IAsyncDisposable
	{
		ISliderRepo SliderRepo { get; }
		IFaqRepo FaqRepo { get; }
		IFaqCategoryRepo FaqCategoryRepo { get; }
		ISocialMediaRepo SocialMediaRepo { get; }
		IAboutUsRepo AboutUsRepo { get; }
		IContactUsRepo ContactUsRepo { get; }
		IRepository<T> GetRepo<T>() where T : class;
	}
}
