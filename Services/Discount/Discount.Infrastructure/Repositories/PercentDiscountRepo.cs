using Discount.Application.Repositories;
using Discount.Domain.Entities;
using Discount.Infrastructure.Persist;
using Discount.Infrastructure.Persist.DAOs;
using Services.Shared.Contracts;

namespace Discount.Infrastructure.Repositories
{
	public class PercentDiscountRepo : Repository<PercentDiscount, PercentDiscountDAO>, IPercentDiscountRepo
	{
		public PercentDiscountRepo(ApplicationDbContext db, ICustomMapper mapper) : base(db, mapper)
		{
		}
	}
}

