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
