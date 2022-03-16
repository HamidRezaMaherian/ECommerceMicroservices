﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Product.API.Tests.Utils;
using Product.Application.DTOs;
using Product.Application.UnitOfWork;
using Product.Infrastructure.Persist;
using Product.Infrastructure.Persist.Mappings;
using Services.Shared.APIUtils;
using Services.Shared.AppUtils;
using Services.Shared.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Product.API.Tests.Integration
{
	[TestFixture]
	public class ProductEndPointsTests
	{
		private HttpRequestHelper _httpClient;
		private IUnitOfWork _unitOfWork;
		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			string dbName = "test_db";
			var dbContext = MockActions.MockDbContext(dbName);

			_unitOfWork = new UnitOfWork(dbContext,
				UtilsExtension.CreateMapper(new PersistMapperProfile()));

			var httpClient = new TestingWebAppFactory<Program>(s =>
			{
				var dbContextConfiguration = s.SingleOrDefault(opt => opt.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
				if (dbContextConfiguration != null)
					s.Remove(dbContextConfiguration);
				s.AddDbContextPool<ApplicationDbContext>(opt => opt.UseInMemoryDatabase(dbName));
			}).CreateClient();
			_httpClient = new HttpRequestHelper(httpClient);
		}
		[TearDown]
		public void TearDown()
		{
			var products = _unitOfWork.ProductRepo.Get();
			foreach (var item in products)
			{
				_unitOfWork.ProductRepo.Delete(item);
			}
			_unitOfWork.Save();
		}
		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			_httpClient.Dispose();
			_unitOfWork.Dispose();
		}
		[TestCase("/product/getAll")]
		public void GetMethods_ReturnJsonMediaType(string endpoint)
		{
			var res = _httpClient.GetAsync(endpoint).Result;
			Assert.AreEqual("application/json", res.Content?.Headers?.ContentType?.MediaType);
		}
		[TestCase("/product/getAll")]
		public void GetMethods_Return200Status(string endpoint)
		{
			var res = _httpClient.GetAsync(endpoint).Result;
			Assert.AreEqual(res.StatusCode, HttpStatusCode.OK);
		}
		[Test]
		public void GetAll_ReturnAllProducts()
		{
			var res = _httpClient.GetAsync("/product/getall").Result;

			var resList = JsonHelper.Parse<IEnumerable<Domain.Entities.Product>>(res.Content.ReadAsStringAsync().Result);
			CollectionAssert.AreEquivalent(resList.Select(i => i.Id),
				_unitOfWork.ProductRepo.Get().Select(i => i.Id));
		}
		[Test]
		public void Create_PassValidObject_AddObject()
		{
			var product = new ProductDTO()
			{
				Name = "test",
				CategoryId = Guid.NewGuid().ToString(),
				CreatedDateTime = DateTime.Now,
				Description = "no desc",
				ShortDesc = "no description",
				MainImagePath = "no path",
				UnitPrice = 29348
			};
			var res = _httpClient.Post("/product/create", product);

			Assert.AreEqual(res.StatusCode, HttpStatusCode.OK);

			Assert.IsTrue(_unitOfWork.ProductRepo.Exists(i => i.Name == product.Name));
		}
		[Test]
		public void Create_PassInvalidObject_ReturnBadRequest()
		{
			var product = new ProductDTO()
			{
				CategoryId = Guid.NewGuid().ToString(),
				CreatedDateTime = DateTime.Now,
				MainImagePath = "no path",
			};
			var res = _httpClient.Post("/product/create", product);

			Assert.AreEqual(res.StatusCode, HttpStatusCode.BadRequest);
			Assert.IsFalse(_unitOfWork.ProductRepo.Exists(i => i.Name == "test"));
		}
		[Test]
		public void Update_PassValidObject_UpdateObject()
		{
			var product = new Domain.Entities.Product()
			{
				Name = "test",
				CategoryId = Guid.NewGuid().ToString(),
				CreatedDateTime = DateTime.Now,
				Description = "no desc",
				ShortDesc = "no description",
				MainImagePath = "no path",
				UnitPrice = 29348
			};
			_unitOfWork.ProductRepo.Add(product);
			_unitOfWork.Save();
			product.Name = "test2";
			var res = _httpClient.Put("/product/update", product);
			var updatedProduct = _unitOfWork.ProductRepo.Get(product.Id);

			Assert.AreEqual(res.StatusCode, HttpStatusCode.OK);
			Assert.AreEqual(updatedProduct.Name, product.Name);
		}
		[Test]
		public void Update_PassInvalidObject_ReturnBadRequest()
		{
			var product = new Domain.Entities.Product()
			{
				Name = "test",
				CategoryId = Guid.NewGuid().ToString(),
				CreatedDateTime = DateTime.Now,
				Description = "no desc",
				ShortDesc = "no description",
				MainImagePath = "no path",
				UnitPrice = 29348
			};
			_unitOfWork.ProductRepo.Add(product);
			_unitOfWork.Save();
			product.Name = "test2";
			var res = _httpClient.Put("/product/update", new
			{
				Name = "test",
				CategoryId = Guid.NewGuid().ToString(),
				CreatedDateTime = DateTime.Now,
			});
			var updatedProduct = _unitOfWork.ProductRepo.Get(product.Id);

			Assert.AreEqual(res.StatusCode, HttpStatusCode.BadRequest);
			Assert.AreNotEqual(updatedProduct.Name, product.Name);
		}
		[Test]
		public void Delete_PassValidId_DeleteRecord()
		{
			var product = new Domain.Entities.Product()
			{
				Name = "test",
				CategoryId = Guid.NewGuid().ToString(),
				CreatedDateTime = DateTime.Now,
				Description = "no desc",
				ShortDesc = "no description",
				MainImagePath = "no path",
				UnitPrice = 29348
			};
			_unitOfWork.ProductRepo.Add(product);
			_unitOfWork.Save();
			var res = _httpClient.Delete($"/product/delete/{product.Id}");
			Assert.IsNull(_unitOfWork.ProductRepo.Get(product.Id));
			Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
		}
		[Test]
		public void Delete_PassInvalidId_ReturnNotFound()
		{
			var fakeId = Guid.NewGuid().ToString();
			var res = _httpClient.Delete($"/product/delete/{fakeId}");
			Assert.AreEqual(HttpStatusCode.NotFound, res.StatusCode);
		}
	}
}
