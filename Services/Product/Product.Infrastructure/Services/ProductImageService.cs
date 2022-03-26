using Product.Application.DTOs;
using Product.Application.Services;
using Product.Application.Tools;
using Product.Application.UnitOfWork;

namespace Product.Infrastructure.Services
{
	public class ProductImageService : GenericActiveService<Domain.Entities.ProductImage, ProductImageDTO>, IProductImageService
	{
		public ProductImageService(IUnitOfWork unitOfWork, ICustomMapper mapper) : base(unitOfWork, mapper)
		{
		}
	}
}
