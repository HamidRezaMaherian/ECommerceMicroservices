using Identity.API.Data;
using Identity.API.Tests.Utils;
using IdentityModel.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Identity.API.Tests.Integration
{
	[TestFixture]
	public class EndPointsTests
	{
		private HttpClient _appClient;
		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			var dbName = "test_db";
			_appClient = new TestingWebAppFactory<Program>(s =>
			{
				var dbContextConfiguration = s.SingleOrDefault(opt => opt.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
				if (dbContextConfiguration != null)
					s.Remove(dbContextConfiguration);
				s.AddDbContextPool<ApplicationDbContext>(opt => opt.UseInMemoryDatabase(dbName));
			}).CreateClient();
		}
		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			_appClient.Dispose();
		}
		[Test]
		public void DiscoveryEndpoint_ReturnOk()
		{
			var res = _appClient.GetDiscoveryDocumentAsync().Result;
			Console.WriteLine(res.HttpResponse.Content.ReadAsStringAsync().Result);
			Assert.AreEqual(HttpStatusCode.OK, res.HttpStatusCode);
		}
		[Test]
		public void TokenEndpoint_GetToken()
		{
			var res = _appClient.RequestTokenAsync(new TokenRequest()
			{
				GrantType = "client_credentials",
				Address = "/connect/token",
				ClientId = "client",
				ClientSecret = "secret",
			}).Result;
			Console.WriteLine(res.AccessToken);
			Assert.AreEqual(HttpStatusCode.OK, res.HttpStatusCode);
		}
	}
}
