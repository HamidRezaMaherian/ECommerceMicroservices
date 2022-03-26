using Inventory.Application.DTOs;
using Inventory.Application.Services;
using Inventory.Application.Tools;
using Inventory.Application.UnitOfWork;
using Inventory.Domain.Entities;

namespace Inventory.Infrastructure.Services
{
	public class StoreService : GenericActiveService<Store, StoreDTO>, IStoreService
	{
		public StoreService(IUnitOfWork unitOfWork, ICustomMapper mapper) : base(unitOfWork, mapper)
		{
		}
	}
}
