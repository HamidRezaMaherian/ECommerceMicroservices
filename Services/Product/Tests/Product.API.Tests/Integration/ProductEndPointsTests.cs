using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Product.API.Controllers;
using Product.Application.DTOs;
using Product.Application.Services;
using Services.Shared.Tests;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Product.API.Tests.Integration
{
	[TestFixture]
	public class ProductEndPointsTests
	{
		private HttpClient _httpClient;
		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_httpClient = new TestingWebAppFactory<Program>().CreateClient();
		}
		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			_httpClient.Dispose();
		}
		[Test]
		public void GetAll_ReturnAllProducts()
		{
			var res = _httpClient.GetAsync("/product/getall").Result;
			Assert.AreEqual(res.StatusCode, 200);
		}
	}
}
