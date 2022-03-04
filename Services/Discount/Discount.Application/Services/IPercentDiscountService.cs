using Discount.Application.DTOs;
using Discount.Domain.Entities;
using System.Linq.Expressions;

namespace Discount.Application.Services;

public interface IPercentDiscountService:IEntityBaseService<PercentDiscount, PercentDiscountDTO>
{
}

