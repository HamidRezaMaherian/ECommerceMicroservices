using AutoMapper;
using Inventory.Application.DTOs;
using Inventory.Application.Services;
using Inventory.Application.UnitOfWork;
using Inventory.Domain.Entities;

namespace Inventory.Infrastructure.Services
{
	public class StockService : GenericActiveService<Stock, StockDTO>, IStockService
	{
		public StockService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
		{
		}
	}
}
