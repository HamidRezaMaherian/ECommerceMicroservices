using Order.Application.DTOs;

namespace Order.Application.Services
{
	public interface IProductService : IEntityBaseService<Domain.Entities.Product, ProductDTO>
	{
	}
}
