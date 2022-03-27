using Order.Application.Repositories;
using Order.Application.Tools;
using Order.Infrastructure.Persist;
using Order.Infrastructure.Persist.DAOs;

namespace Order.Infrastructure.Repositories
{
	public class ProductRepo : Repository<Domain.Entities.Product, ProductDAO>
		, IProductRepo
	{
		public ProductRepo(ApplicationDbContext db, ICustomMapper mapper) : base(db, mapper) { }
	}
}
