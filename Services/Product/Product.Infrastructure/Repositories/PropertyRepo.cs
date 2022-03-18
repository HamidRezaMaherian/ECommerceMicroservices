using Product.Application.Repositories;
using Product.Domain.Entities;
using Product.Infrastructure.Persist;
using Product.Infrastructure.Persist.DAOs;
using Services.Shared.Contracts;

namespace Product.Infrastructure.Repositories
{
	public class PropertyRepo : Repository<Property, PropertyDAO>
		, IPropertyRepo
	{
		public PropertyRepo(ApplicationDbContext db, ICustomMapper mapper) : base(db, mapper) { }
	}
}
