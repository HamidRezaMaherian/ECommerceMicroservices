using Order.Application.DTOs;
using Order.Application.Services;
using Order.Application.Tools;
using Order.Application.UnitOfWork;

namespace Order.Infrastructure.Services
{
	public class BrandService : GenericActiveService<Domain.Entities.Brand, BrandDTO>, IBrandService
	{
		public BrandService(IUnitOfWork unitOfWork, ICustomMapper mapper) : base(unitOfWork, mapper)
		{
		}
	}
}
