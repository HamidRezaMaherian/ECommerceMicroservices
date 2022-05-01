using Identity.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Tests.Utils
{
	public static class MockActions
	{
		public static ApplicationDbContext MockDbContext(string dbName)
		{
			var dbOptionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
			dbOptionsBuilder.UseInMemoryDatabase(dbName);
			return new ApplicationDbContext(dbOptionsBuilder.Options);
		}
	}
}
