using Product.Application.Repositories;
using Product.Infrastructure.Persist;
using Product.Infrastructure.Persist.DAOs;

namespace Product.Infrastructure.Repositories
{
	public class ProductRepo : Repository<Domain.Entities.Product, ProductDAO>, IProductRepo
	{
		public ProductRepo(ApplicationDbContext db) : base(db) { }
	}
}
