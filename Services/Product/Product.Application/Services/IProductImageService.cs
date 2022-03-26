using Product.Application.DTOs;
using Product.Domain.Entities;

namespace Product.Application.Services
{
	public interface IProductImageService : IEntityBaseService<ProductImage, ProductImageDTO>
	{
	}
}
