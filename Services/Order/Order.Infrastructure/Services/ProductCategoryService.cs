using Order.Application.DTOs;
using Order.Application.Services;
using Order.Application.Tools;
using Order.Application.UnitOfWork;

namespace Order.Infrastructure.Services
{
	public class ProductCategoryService : GenericActiveService<Domain.Entities.ProductCategory, ProductCategoryDTO>, IProductCategoryService
	{
		public ProductCategoryService(IUnitOfWork unitOfWork, ICustomMapper mapper) : base(unitOfWork, mapper)
		{
		}
	}
}
