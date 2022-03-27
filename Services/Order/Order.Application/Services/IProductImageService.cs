using Order.Application.DTOs;
using Order.Domain.Entities;

namespace Order.Application.Services
{
	public interface IProductImageService : IEntityBaseService<ProductImage, ProductImageDTO>
	{
	}
}
