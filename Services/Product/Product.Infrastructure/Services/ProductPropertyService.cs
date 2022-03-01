using AutoMapper;
using Product.Application.DTOs;
using Product.Application.Services;
using Product.Application.UnitOfWork;

namespace Product.Infrastructure.Services
{
	public class ProductPropertyService : GenericBaseService<Domain.Entities.ProductProperty, ProductPropertyDTO>, IProductPropertyService
	{
		public ProductPropertyService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
		{
		}
	}
}
