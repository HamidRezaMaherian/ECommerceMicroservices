using Inventory.Application.UnitOfWork;
using Inventory.Infrastructure.Persist;
using Mongo2Go;
using MongoDB.Driver;
using Services.Shared.Contracts;

namespace Inventory.API.Tests.Utils
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
	}
}
