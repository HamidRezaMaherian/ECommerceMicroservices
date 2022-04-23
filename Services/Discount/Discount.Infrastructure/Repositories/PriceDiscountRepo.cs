using Discount.Application.Repositories;
using Discount.Application.Services;
using Discount.Domain.Entities;
using Discount.Infrastructure.Persist;
using Discount.Infrastructure.Persist.DAOs;

namespace Discount.Infrastructure.Repositories
{
	public class PriceDiscountRepo : Repository<PriceDiscount, PriceDiscountDAO>, IPriceDiscountRepo
	{
		public PriceDiscountRepo(ApplicationDbContext db, ICustomMapper mapper) : base(db, mapper)
		{
		}
		public override void Add(PriceDiscount entity)
		{
			entity.Id = null;
			base.Add(entity);
		}
	}
}
