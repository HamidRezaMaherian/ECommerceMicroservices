using Inventory.Application.UnitOfWork;
using Inventory.Infrastructure.Persist;
using MongoDB.Driver;
using Services.Shared.Contracts;

namespace Inventory.API.Tests.Utils
{
	public static class MockActions
	{
		public static ApplicationDbContext MockDbContext()
		{
			var mongoClient = new MongoClient("mongodb//localhost:27017");
			return new ApplicationDbContext(mongoClient, "test-db");
		}
		public static IUnitOfWork MockUnitOfWork(ApplicationDbContext db, ICustomMapper mapper)
		{
			return new UnitOfWork(db, mapper);
		}

	}
}
