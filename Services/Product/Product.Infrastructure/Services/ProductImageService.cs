using AutoMapper;
using Product.Application.DTOs;
using Product.Application.Services;
using Product.Application.UnitOfWork;

namespace Product.Infrastructure.Services
{
	public class ProductImageService : GenericActiveService<Domain.Entities.ProductImage, ProductImageDTO>, IProductImageService
	{
		public ProductImageService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
		{
		}
	}
}
