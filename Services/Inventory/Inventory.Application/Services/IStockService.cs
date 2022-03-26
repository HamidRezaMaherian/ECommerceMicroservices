using Inventory.Application.DTOs;
using Inventory.Domain.Entities;

namespace Inventory.Application.Services;

public interface IStockService : IEntityBaseService<Stock, StockDTO>
{
}

