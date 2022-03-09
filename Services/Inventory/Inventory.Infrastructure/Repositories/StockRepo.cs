using AutoMapper;
using Inventory.Application.Repositories;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.Persist;
using Inventory.Infrastructure.Persist.DAOs;

namespace Inventory.Infrastructure.Repositories
{
	public class StockRepo : Repository<Stock, StockDAO>, IStockRepo
	{
		public StockRepo(ApplicationDbContext db, IMapper mapper) : base(db, mapper) { }
	}
}
