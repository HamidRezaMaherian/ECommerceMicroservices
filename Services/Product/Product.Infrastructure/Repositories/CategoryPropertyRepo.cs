using Product.Application.Repositories;
using Product.Application.Tools;
using Product.Domain.Entities;
using Product.Infrastructure.Persist;
using Product.Infrastructure.Persist.DAOs;

namespace Product.Infrastructure.Repositories
{
	public class CategoryPropertyRepo : Repository<CategoryProperty, CategoryPropertyDAO>,
		ICategoryPropertyRepo
	{
		public CategoryPropertyRepo(ApplicationDbContext db, ICustomMapper mapper) : base(db, mapper) { }
	}
}
