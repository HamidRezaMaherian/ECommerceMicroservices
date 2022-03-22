using Inventory.Domain.Entities;
using Inventory.Infrastructure.Persist.DAOs;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Inventory.Infrastructure.Persist
{
	public class ApplicationDbContext : IDisposable, IAsyncDisposable
	{
		public ApplicationDbContext(MongoClient client,string dbName)
		{
			DataBase = client.GetDatabase(dbName);
		}

		public IMongoDatabase DataBase { get; }
		public void Dispose()
		{
			//DataBase.Client.Cluster.Dispose();
		}

		public ValueTask DisposeAsync()
		{
			return ValueTask.CompletedTask;
			//DataBase.Client.Cluster.Dispose();
			//await Task.CompletedTask;
		}
	}
}
