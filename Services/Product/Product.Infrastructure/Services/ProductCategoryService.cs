using Product.Application.DTOs;
using Product.Application.Services;
using Product.Application.Tools;
using Product.Application.UnitOfWork;

namespace Product.Infrastructure.Services
{
	public class ProductCategoryService : GenericActiveService<Domain.Entities.ProductCategory, ProductCategoryDTO>, IProductCategoryService
	{
		public ProductCategoryService(IUnitOfWork unitOfWork, ICustomMapper mapper) : base(unitOfWork, mapper)
		{
		}
	}
}
