using AutoMapper;
using Discount.Application.Repositories;
using Discount.Domain.Entities;
using Discount.Infrastructure.Persist;
using Discount.Infrastructure.Persist.DAOs;

namespace Discount.Infrastructure.Repositories
{
	public class PercentDiscountRepo : Repository<PercentDiscount, PercentDiscountDAO>, IPercentDiscountRepo
	{
		public PercentDiscountRepo(ApplicationDbContext db, IMapper mapper) : base(db, mapper)
		{
		}
	}
}

