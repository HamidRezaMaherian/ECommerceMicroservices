using AutoMapper;
using Inventory.Application.Repositories;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.Persist;
using Inventory.Infrastructure.Persist.DAOs;

namespace Inventory.Infrastructure.Repositories
{
	public class StoreRepo : Repository<Store, StoreDAO>,IStoreRepo
	{
		public StoreRepo(ApplicationDbContext db, IMapper mapper) : base(db, mapper)
		{
		}
	}
}
