using Order.Application.DTOs;
using Order.Application.Services;
using Order.Application.Tools;
using Order.Application.UnitOfWork;

namespace Order.Infrastructure.Services
{
	public class CategoryPropertyService : GenericBaseService<Domain.Entities.CategoryProperty, CategoryPropertyDTO>, ICategoryPropertyService
	{
		public CategoryPropertyService(IUnitOfWork unitOfWork, ICustomMapper mapper) : base(unitOfWork, mapper)
		{
		}
	}
}
