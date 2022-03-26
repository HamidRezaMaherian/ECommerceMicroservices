using Product.Application.Repositories;
using Product.Application.Tools;
using Product.Domain.Entities;
using Product.Infrastructure.Persist;
using Product.Infrastructure.Persist.DAOs;

namespace Product.Infrastructure.Repositories
{
	public class ProductPropertyRepo : Repository<ProductProperty, ProductPropertyDAO>
		, IProductPropertyRepo
	{
		public ProductPropertyRepo(ApplicationDbContext db, ICustomMapper mapper) : base(db, mapper) { }
	}
}
