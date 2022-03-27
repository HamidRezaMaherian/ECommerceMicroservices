using Order.Application.DTOs;
using Order.Domain.Entities;

namespace Order.Application.Services;

public interface IProductCategoryService : IEntityBaseService<ProductCategory, ProductCategoryDTO>
{
}
