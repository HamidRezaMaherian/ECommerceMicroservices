using Microsoft.EntityFrameworkCore;
using Product.Application.Tools;
using Product.Application.UnitOfWork;
using Product.Infrastructure.Persist;

namespace Product.API.Tests.Utils
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
