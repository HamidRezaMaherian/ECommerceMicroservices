using Product.Application.DTOs;
using Product.Application.Services;
using Product.Application.Tools;
using Product.Application.UnitOfWork;

namespace Product.Infrastructure.Services
{
	public class CategoryPropertyService : GenericBaseService<Domain.Entities.CategoryProperty, CategoryPropertyDTO>, ICategoryPropertyService
	{
		public CategoryPropertyService(IUnitOfWork unitOfWork, ICustomMapper mapper) : base(unitOfWork, mapper)
		{
		}
	}
}
