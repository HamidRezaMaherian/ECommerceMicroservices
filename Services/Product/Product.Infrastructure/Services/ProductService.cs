using Product.Application.DTOs;
using Product.Application.Services;
using Product.Application.UnitOfWork;
using Services.Shared.Contracts;

namespace Product.Infrastructure.Services
{
	public class ProductService : GenericActiveService<Domain.Entities.Product, ProductDTO>, IProductService
	{
		public ProductService(IUnitOfWork unitOfWork, ICustomMapper mapper) : base(unitOfWork, mapper)
		{
		}
	}
}
