using Mongo2Go;
using MongoDB.Driver;
using Moq;
using System.Collections.Generic;
using System.Linq;
using UI.Application.DTOs;
using UI.Application.Services;
using UI.Application.Tools;
using UI.Application.UnitOfWork;
using UI.Domain.Entities;
using UI.Infrastructure.Persist;

namespace UI.API.Tests.Utils
{
	public static class MockActions
	{
		public static ApplicationDbContext MockDbContext(MongoDbRunner mongoRunner)
		{
			var mongoClient = new MongoClient(mongoRunner.ConnectionString);
			return new ApplicationDbContext(mongoClient, "test-db");
		}
		public static IUnitOfWork MockUnitOfWork(ApplicationDbContext db, ICustomMapper mapper)
		{
			return new UnitOfWork(db, mapper);
		}
		public static IAboutUsService MockAboutUsService(IList<AboutUs> list)
		{
			var store = new Mock<IAboutUsService>();
			var mapper = TestUtilsExtension.CreateMapper<AboutUs, AboutUsDTO>();
			var repository = TestUtilsExtension.MockAction<AboutUs, AboutUsDTO>.MockRepository(list);

			store.Setup(i => i.FirstOrDefault())
				.Returns(list.First());

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
				.Returns(list.First());

			store.Setup(i => i.Update(It.IsAny<ContactUsDTO>()))
				.Callback<ContactUsDTO>(c => repository.Update(mapper.Map<ContactUs>(c)));

			return store.Object;

		}
	}
}
