using Product.Application.Repositories;
using Product.Application.Tools;
using Product.Infrastructure.Persist;
using Product.Infrastructure.Persist.DAOs;

namespace Product.Infrastructure.Repositories
{
	public class ProductRepo : Repository<Domain.Entities.Product, ProductDAO>
		, IProductRepo
	{
		public ProductRepo(ApplicationDbContext db, ICustomMapper mapper) : base(db, mapper) { }
	}
}
