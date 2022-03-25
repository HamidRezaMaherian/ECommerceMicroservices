using Discount.Application.Repositories;
using Discount.Domain.Entities;
using Discount.Infrastructure.Persist;
using Discount.Infrastructure.Persist.DAOs;
using Services.Shared.Contracts;

namespace Discount.Infrastructure.Repositories
{
	public class PriceDiscountRepo : Repository<PriceDiscount, PriceDiscountDAO>, IPriceDiscountRepo
	{
		public PriceDiscountRepo(ApplicationDbContext db, ICustomMapper mapper) : base(db, mapper)
		{
		}
	}
}
