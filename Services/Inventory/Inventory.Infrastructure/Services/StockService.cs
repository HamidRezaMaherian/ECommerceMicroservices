using Inventory.Application.DTOs;
using Inventory.Application.Services;
using Inventory.Application.UnitOfWork;
using Inventory.Domain.Entities;
using Services.Shared.Contracts;

namespace Inventory.Infrastructure.Services
{
	public class StockService : GenericActiveService<Stock, StockDTO>, IStockService
	{
		public StockService(IUnitOfWork unitOfWork, ICustomMapper mapper) : base(unitOfWork, mapper)
		{
		}
	}
}
