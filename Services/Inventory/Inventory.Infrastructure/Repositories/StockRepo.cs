using AutoMapper;
using Inventory.Application.Repositories;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.Persist;
using Inventory.Infrastructure.Persist.DAOs;
using Services.Shared.Contracts;

namespace Inventory.Infrastructure.Repositories
{
	public class StockRepo : Repository<Stock, StockDAO>, IStockRepo
	{
		public StockRepo(ApplicationDbContext db, ICustomMapper mapper) : base(db, mapper) { }
	}
}
