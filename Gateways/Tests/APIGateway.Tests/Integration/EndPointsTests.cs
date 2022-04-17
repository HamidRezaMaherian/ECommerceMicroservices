using APIGateway.API.Tests.Utils;
using NUnit.Framework;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace APIGateway.API.Tests.Integration
{
	[TestFixture]
	public class EndPointsTests
	{
		private HttpClient _appClient;
		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_appClient = new TestingWebAppFactory<Program>().CreateClient();
		}
		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			_appClient.Dispose();
		}
		[Test]
		public void Initialized_ReturnTrue()
		{
			var res = _appClient.GetAsync("/home").Result;
			res.EnsureSuccessStatusCode();
		}
	}
}
