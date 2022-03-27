using Order.Application.Repositories;
using Order.Application.Tools;
using Order.Domain.Entities;
using Order.Infrastructure.Persist;
using Order.Infrastructure.Persist.DAOs;

namespace Order.Infrastructure.Repositories
{
	public class ProductCategoryRepo : Repository<ProductCategory, ProductCategoryDAO>
		, IProductCategoryRepo
	{
		public ProductCategoryRepo(ApplicationDbContext db, ICustomMapper mapper) : base(db, mapper) { }
	}
}
