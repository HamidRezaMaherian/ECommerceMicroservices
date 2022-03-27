using Order.Application.DTOs;
using Order.Application.Services;
using Order.Application.Tools;
using Order.Application.UnitOfWork;

namespace Order.Infrastructure.Services
{
	public class ProductImageService : GenericActiveService<Domain.Entities.ProductImage, ProductImageDTO>, IProductImageService
	{
		public ProductImageService(IUnitOfWork unitOfWork, ICustomMapper mapper) : base(unitOfWork, mapper)
		{
		}
	}
}
