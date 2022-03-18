using AutoMapper;
using Product.Application.Repositories;
using Product.Domain.Entities;
using Product.Infrastructure.Persist;
using Product.Infrastructure.Persist.DAOs;
using Services.Shared.Contracts;

namespace Product.Infrastructure.Repositories
{
	public class ProductCategoryRepo : Repository<ProductCategory, ProductCategoryDAO>
		, IProductCategoryRepo
	{
		public ProductCategoryRepo(ApplicationDbContext db, ICustomMapper mapper) : base(db, mapper) { }
	}
}
