using AutoMapper;
using Product.Application.Repositories;
using Product.Domain.Entities;
using Product.Infrastructure.Persist;
using Product.Infrastructure.Persist.DAOs;

namespace Product.Infrastructure.Repositories
{
	public class PropertyRepo : Repository<Property, PropertyDAO>
		, IPropertyRepo
	{
		public PropertyRepo(ApplicationDbContext db,IMapper mapper) : base(db,mapper) { }
	}
}
