using Discount.Application.Services;
using Discount.Application.UnitOfWork;
using Discount.Infrastructure.Persist;
using Microsoft.EntityFrameworkCore;

namespace Discount.API.Tests.Utils
{
	public static class MockActions
	{
		public static ApplicationDbContext MockDbContext(string dbName)
		{
			var dbOptionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
			dbOptionsBuilder.UseInMemoryDatabase(dbName);
			return new ApplicationDbContext(dbOptionsBuilder.Options);
		}
		public static IUnitOfWork MockUnitOfWork(string dbName, ICustomMapper mapper)
		{
			return new UnitOfWork(MockDbContext(dbName), mapper);
		}
	}
}
