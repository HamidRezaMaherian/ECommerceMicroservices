using Product.Domain.Entities;

namespace Product.Application.Services
{
	public interface IBrandService : IEntityBaseService<BrandDTO>
	{
		new IEnumerable<BrandDTO> GetAll();
		new BrandDTO GetById(object id);
		new IEnumerable<BrandDTO> GetAllActive();
	}
}
