using Product.Application.DTOs;
using Services.Shared.Contracts;

namespace Product.Application.Services
{
	public interface IProductService : IEntityBaseService<Domain.Entities.Product,ProductDTO>
	{
	}
}
