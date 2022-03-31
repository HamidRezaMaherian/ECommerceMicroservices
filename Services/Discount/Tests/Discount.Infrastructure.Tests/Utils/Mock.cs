using Discount.Infrastructure.Persist;
using Microsoft.EntityFrameworkCore;

namespace Discount.Infrastructure.Tests.Utils
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
