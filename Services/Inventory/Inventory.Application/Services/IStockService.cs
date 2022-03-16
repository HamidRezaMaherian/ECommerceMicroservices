﻿using Inventory.Application.DTOs;
using Inventory.Domain.Entities;
using Services.Shared.Contracts;

namespace Inventory.Application.Services;

public interface IStockService : IEntityBaseService<Stock, StockDTO>
{
}
