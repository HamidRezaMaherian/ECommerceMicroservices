using Order.Application.Repositories;
using Order.Application.Tools;
using Order.Domain.Entities;
using Order.Infrastructure.Persist;
using Order.Infrastructure.Persist.DAOs;

namespace Order.Infrastructure.Repositories
{
	public class PaymentRepo : Repository<Payment, PaymentDAO>, IPaymentRepo
	{
		public PaymentRepo(ApplicationDbContext db, ICustomMapper mapper) : base(db, mapper)
		{
		}
		public override void Add(Payment entity)
		{
			entity.Id = null;
			base.Add(entity);
		}
	}
}
