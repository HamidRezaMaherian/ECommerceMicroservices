using Discount.Application.DTOs;
using Discount.Domain.Entities;
using Services.Shared.Contracts;

namespace Discount.Application.Services;

public interface IPercentDiscountService : IEntityBaseService<PercentDiscount, PercentDiscountDTO>
{
}

