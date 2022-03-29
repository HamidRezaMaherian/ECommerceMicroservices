using Duende.IdentityServer.EntityFramework.Options;
using Identity.API.Data;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Identity.API.Tests.Utils
{
	public static class MockActions
	{	
		public static ApplicationDbContext MockDbContext(string dbName)
		{
			var dbOptionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
			dbOptionsBuilder.UseInMemoryDatabase(dbName);
			var opStoreOptions = Options.Create<OperationalStoreOptions>(new OperationalStoreOptions());
			return new ApplicationDbContext(dbOptionsBuilder.Options, opStoreOptions);
		}
	}
}
