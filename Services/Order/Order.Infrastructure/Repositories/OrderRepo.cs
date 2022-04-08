using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Order.Application.Exceptions;
using Order.Application.Repositories;
using Order.Application.Tools;
using Order.Domain.Entities;
using Order.Infrastructure.Persist;
using Order.Infrastructure.Persist.DAOs;
using Services.Shared.AppUtils;
using System.Linq.Expressions;

namespace Order.Infrastructure.Repositories
{
	public class OrderRepo : Repository<Domain.Entities.Order, OrderDAO>, IOrderRepo
	{
		public OrderRepo(ApplicationDbContext db, ICustomMapper mapper) : base(db, mapper)
		{
		}
	}
}
