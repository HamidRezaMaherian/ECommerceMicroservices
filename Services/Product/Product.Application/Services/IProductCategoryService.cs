using Product.Application.DTOs;
using Product.Domain.Entities;

namespace Product.Application.Services;

public interface IProductCategoryService : IEntityBaseService<ProductCategory,ProductCategoryDTO>
{
}
