using Microsoft.EntityFrameworkCore;
using Order.Infrastructure.Persist;

namespace Order.Infrastructure.Tests.Utils
{
	public static class MockActions
	{
		public static ApplicationDbContext MockDbContext(string getName)
		{
			var dbOptionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
			dbOptionsBuilder.UseInMemoryDatabase("TestDb");
			return new ApplicationDbContext(dbOptionsBuilder.Options);
		}
	}
}
