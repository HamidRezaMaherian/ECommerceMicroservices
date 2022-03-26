using Product.Application.DTOs;
using Product.Domain.Entities;

namespace Product.Application.Services
{
	public interface IBrandService : IEntityBaseService<Brand, BrandDTO>
	{
	}
}
