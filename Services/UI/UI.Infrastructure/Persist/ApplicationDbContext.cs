using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using UI.Domain.Entities;
using UI.Infrastructure.Persist.DAOs;

namespace UI.Infrastructure.Persist
{
	public class ApplicationDbContext : IDisposable, IAsyncDisposable
	{
		private IMongoCollection<FaqDAO> _faqs;
		private IMongoCollection<FaqCategoryDAO> _faqCategories;
		private IMongoCollection<AboutUsDAO> _aboutUs;
		private IMongoCollection<ContactUsDAO> _contactUs;
		private IMongoCollection<SliderDAO> _sliders;
		private IMongoCollection<SocialMediaDAO> _socialMedias;

		public ApplicationDbContext(MongoClient client, string dbName)
		{
			DataBase = client.GetDatabase(dbName);
		}
		public IMongoCollection<FaqDAO> Faqs
		{
			get
			{
				_faqs ??= DataBase.GetCollection<FaqDAO>(nameof(FAQ));
				return _faqs;
			}
		}
		public IMongoCollection<FaqCategoryDAO> FaqCategories
		{
			get
			{
				_faqCategories ??= DataBase.GetCollection<FaqCategoryDAO>(nameof(FaqCategory));
				return _faqCategories;
			}
		}

		public IMongoCollection<AboutUsDAO> AboutUs
		{
			get
			{
				_aboutUs ??= DataBase.GetCollection<AboutUsDAO>(nameof(Domain.Entities.AboutUs));
				return _aboutUs;
			}
		}

		public IMongoCollection<ContactUsDAO> ContactUs
		{
			get
			{
				_contactUs ??= DataBase.GetCollection<ContactUsDAO>(nameof(Domain.Entities.ContactUs));
				return _contactUs;
			}
		}

		public IMongoCollection<SliderDAO> Sliders
		{
			get
			{
				_sliders  ??= DataBase.GetCollection<SliderDAO>(nameof(Slider));
				return _sliders ;
			}
		}

		public IMongoCollection<SocialMediaDAO> SocialMedias
		{
			get
			{
				_socialMedias ??= DataBase.GetCollection<SocialMediaDAO>(nameof(SocialMedia));
				return _socialMedias;
			}
		}

		public IMongoDatabase DataBase { get; }
		public void Dispose()
		{
		}

		public async ValueTask DisposeAsync()
		{
			await ValueTask.CompletedTask;
		}
	}
}
