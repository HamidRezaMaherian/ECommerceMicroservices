using Order.Application.Repositories;
using Order.Application.Tools;
using Order.Domain.Entities;
using Order.Infrastructure.Persist;
using Order.Infrastructure.Persist.DAOs;

namespace Order.Infrastructure.Repositories
{
	public class BrandRepo : Repository<Brand, BrandDAO>, IBrandRepo
	{
		public BrandRepo(ApplicationDbContext db, ICustomMapper mapper) : base(db, mapper) { }
	}
}
