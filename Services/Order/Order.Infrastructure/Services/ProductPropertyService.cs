using Order.Application.DTOs;
using Order.Application.Services;
using Order.Application.Tools;
using Order.Application.UnitOfWork;

namespace Order.Infrastructure.Services
{
	public class ProductPropertyService : GenericBaseService<Domain.Entities.ProductProperty, ProductPropertyDTO>, IProductPropertyService
	{
		public ProductPropertyService(IUnitOfWork unitOfWork, ICustomMapper mapper) : base(unitOfWork, mapper)
		{
		}
	}
}
