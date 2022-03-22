using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Moq;
using Services.Shared.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UI.Application.DTOs;
using UI.Application.Services;
using UI.Domain.Entities;
using UI.Infrastructure.Persist;
using UI.Infrastructure.Persist.DAOs;

namespace UI.API.Tests.Utils
{
	public static class MockActions
	{
		public static ApplicationDbContext MockDbContext(string dbName, IDictionary<string, IList<object>> storage)
		{
			var configurationBuilder = new ConfigurationBuilder();
			configurationBuilder.AddInMemoryCollection(new List<KeyValuePair<string, string>>()
			{
				new KeyValuePair<string, string>("ConnectionStrings:DefaultConnection","")
			});
			return new ApplicationDbContext(MockMongoClient(storage).Object, configurationBuilder.Build());
		}
		public static IAboutUsService MockAboutUsService(IList<AboutUs> list)
		{
			var store = new Mock<IAboutUsService>();
			var mapper = TestUtilsExtension.CreateMapper<AboutUs, AboutUsDTO>();
			var repository = TestUtilsExtension.MockAction<AboutUs, AboutUsDTO>.MockRepository(list);

			store.Setup(i => i.FirstOrDefault())
				.Returns(list.FirstOrDefault());

			store.Setup(i => i.Update(It.IsAny<AboutUsDTO>()))
				.Callback<AboutUsDTO>(c => repository.Update(mapper.Map<AboutUs>(c)));

			return store.Object;

		}
		public static IContactUsService MockContactUsService(IList<ContactUs> list)
		{
			var store = new Mock<IContactUsService>();
			var mapper = TestUtilsExtension.CreateMapper<ContactUs, ContactUsDTO>();
			var repository = TestUtilsExtension.MockAction<ContactUs, ContactUsDTO>.MockRepository(list);

			store.Setup(i => i.FirstOrDefault())
				.Returns(list.FirstOrDefault());

			store.Setup(i => i.Update(It.IsAny<ContactUsDTO>()))
				.Callback<ContactUsDTO>(c => repository.Update(mapper.Map<ContactUs>(c)));

			return store.Object;

		}
		#region PrivateMethods
		private static Mock<MongoClient> MockMongoClient(IDictionary<string, IList<object>> storage)
		{
			var store = new Mock<MongoClient>();
			store.Setup(i => i.GetDatabase(It.IsAny<string>(), It.IsAny<MongoDatabaseSettings>()))
				.Returns(MockMongoDatabase(storage).Object);
			return store;
		}

		private static Mock<IMongoDatabase> MockMongoDatabase(IDictionary<string, IList<object>> storage)
		{
			var store = new Mock<IMongoDatabase>();
			//store.Setup(i => i.GetCollection<StockDAO>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>()))
			//	.Returns(MockMongoCollection<StockDAO>(storage).Object);
			//store.Setup(i => i.GetCollection<StoreDAO>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>()))
			//	.Returns(MockMongoCollection<StoreDAO>(storage).Object);
			return store;
		}

		private static Mock<IMongoCollection<T>> MockMongoCollection<T>(IDictionary<string, IList<object>> storage) where T : EntityBaseDAO<string>
		{
			var store = new Mock<IMongoCollection<T>>();
			store.Setup(i => i.InsertOne(It.IsAny<T>(), null, default)).Callback<T>((obj) =>
			{
				storage[nameof(T)].Add(obj);
			});
			store.Setup(i => i.DeleteOne(It.IsAny<Expression<Func<T, bool>>>(), default))
				.Callback<Expression<Func<T, bool>>>((exp) =>
				{
					var doc = storage[nameof(T)].FirstOrDefault(exp);
					storage[nameof(T)].Remove(doc);
				});
			store.Setup(i => i.AsQueryable(null))
				.Returns(() => storage[nameof(T)].AsQueryable());
			store.Setup(i => i.ReplaceOne(It.IsAny<Expression<Func<T, bool>>>(), It.IsAny<T>(), It.IsAny<ReplaceOptions>(), default))
				.Callback<Expression<Func<T, bool>>, T>((exp, replaceObject) =>
				{
					var deleteObject = storage[nameof(T)].FirstOrDefault(exp);
					storage[nameof(T)].Remove(deleteObject);
					storage[nameof(T)].Add(replaceObject);
				});
			return store;
		}
		#endregion
	}
}
