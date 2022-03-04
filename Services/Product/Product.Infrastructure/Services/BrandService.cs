using AutoMapper;
using Product.Application.DTOs;
using Product.Application.Services;
using Product.Application.UnitOfWork;

namespace Product.Infrastructure.Services
{
	public class BrandService : GenericActiveService<Domain.Entities.Brand, BrandDTO>, IBrandService
	{
		public BrandService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
		{
		}
	}
}
