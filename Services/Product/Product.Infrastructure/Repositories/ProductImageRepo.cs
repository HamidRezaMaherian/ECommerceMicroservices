using Product.Application.Repositories;
using Product.Domain.Entities;
using Product.Infrastructure.Persist;
using Product.Infrastructure.Persist.DAOs;
using Services.Shared.Contracts;

namespace Product.Infrastructure.Repositories
{
	public class ProductImageRepo : Repository<ProductImage, ProductImageDAO>
		, IProductImageRepo
	{
		public ProductImageRepo(ApplicationDbContext db, ICustomMapper mapper) : base(db, mapper) { }
	}
}
