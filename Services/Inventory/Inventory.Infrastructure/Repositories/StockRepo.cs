using Inventory.Application.Exceptions;
using Inventory.Application.Repositories;
using Inventory.Application.Tools;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.Persist;
using Inventory.Infrastructure.Persist.DAOs;
using MongoDB.Driver;

namespace Inventory.Infrastructure.Repositories
{
	public class StockRepo : Repository<Stock, StockDAO>, IStockRepo
	{
		public StockRepo(ApplicationDbContext db, ICustomMapper mapper) : base(db, mapper) { }
		public override void Add(Stock entity)
		{
			var storeDbSet = _db.DataBase.GetCollection<StoreDAO>(typeof(Store).Name);
			if (storeDbSet.AsQueryable().Any(i => i.Id == entity.StoreId))
				base.Add(entity);
			else
				throw new InsertOperationException("The given {storeId} not exists");
		}
	}
}
