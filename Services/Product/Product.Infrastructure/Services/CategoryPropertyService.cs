using AutoMapper;
using Product.Application.DTOs;
using Product.Application.Services;
using Product.Application.UnitOfWork;

namespace Product.Infrastructure.Services
{
	public class CategoryPropertyService : GenericBaseService<Domain.Entities.CategoryProperty, CategoryPropertyDTO>, ICategoryPropertyService
	{
		public CategoryPropertyService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
		{
		}
	}
}
