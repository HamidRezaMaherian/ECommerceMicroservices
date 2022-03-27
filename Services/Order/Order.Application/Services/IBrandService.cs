using Order.Application.DTOs;
using Order.Domain.Entities;

namespace Order.Application.Services
{
	public interface IBrandService : IEntityBaseService<Brand, BrandDTO>
	{
	}
}
