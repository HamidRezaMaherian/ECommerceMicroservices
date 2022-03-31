using Inventory.Infrastructure.Persist;
using Mongo2Go;
using MongoDB.Driver;

namespace Inventory.Infrastructure.Tests.Utils
{
	public static class MockActions
	{
		public static ApplicationDbContext MockDbContext(MongoDbRunner mongoRunner)
		{
			var mongoClient = new MongoClient(mongoRunner.ConnectionString);
			return new ApplicationDbContext(mongoClient, "test-db");
		}
	}
}
