using Product.Application.DTOs;

namespace Product.Application.Services
{
	public interface IProductService : IEntityBaseService<Domain.Entities.Product, ProductDTO>
	{
	}
}
