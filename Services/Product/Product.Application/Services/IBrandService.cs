using Product.Application.DTOs;
using Product.Domain.Entities;
using Services.Shared.Contracts;

namespace Product.Application.Services
{
	public interface IBrandService : IEntityBaseService<Brand, BrandDTO>
	{
	}
}
