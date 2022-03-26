using Discount.Application.DTOs;
using Discount.Domain.Entities;

namespace Discount.Application.Services;

public interface IPercentDiscountService : IEntityBaseService<PercentDiscount, PercentDiscountDTO>
{
}

