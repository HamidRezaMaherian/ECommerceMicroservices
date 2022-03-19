using Inventory.Domain.Entities;
using Inventory.Infrastructure.Persist.DAOs;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Inventory.Infrastructure.Persist
{
	public class ApplicationDbContext : IDisposable, IAsyncDisposable
	{
		private IMongoCollection<StockDAO> _stocks;
		private IMongoCollection<StoreDAO> _stores;

		public ApplicationDbContext(MongoClient client,IConfiguration configuration)
		{
			DataBase = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);
		}

		public IMongoDatabase DataBase { get; }
		#region Collections
		public IMongoCollection<StockDAO> Stocks
		{
			get
			{
				_stocks ??= DataBase.GetCollection<StockDAO>(nameof(Stock));
				return _stocks;
			}
		}
		public IMongoCollection<StoreDAO> Stores
		{
			get
			{
				_stores ??= DataBase.GetCollection<StoreDAO>(nameof(Store));
				return _stores;
			}
		}
		#endregion

		public void Dispose()
		{
			DataBase.Client.Cluster.Dispose();
		}

		public async ValueTask DisposeAsync()
		{
			DataBase.Client.Cluster.Dispose();
			await Task.CompletedTask;
		}
	}
}
