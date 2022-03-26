using Inventory.Application.Repositories;
using Inventory.Application.Tools;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.Persist;
using Inventory.Infrastructure.Persist.DAOs;

namespace Inventory.Infrastructure.Repositories
{
	public class StoreRepo : Repository<Store, StoreDAO>, IStoreRepo
	{
		public StoreRepo(ApplicationDbContext db, ICustomMapper mapper) : base(db, mapper)
		{
		}
	}
}
