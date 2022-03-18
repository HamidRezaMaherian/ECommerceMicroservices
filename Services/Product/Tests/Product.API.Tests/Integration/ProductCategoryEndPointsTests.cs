using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Product.API.Tests.Utils;
using Product.Application.DTOs;
using Product.Application.UnitOfWork;
using Product.Domain.Entities;
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
	public class ProductCategoryEndPointsTests
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
			var productCategorys = _unitOfWork.ProductCategoryRepo.Get();
			foreach (var item in productCategorys)
			{
				_unitOfWork.ProductCategoryRepo.Delete(item);
			}
			_unitOfWork.Save();
		}
		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			_httpClient.Dispose();
			_unitOfWork.Dispose();
		}
		[Test]
		public void GetAll_ReturnAllProductCategorys()
		{
			var res = _httpClient.Get("/productCategory/getall");

			var resList = JsonHelper.Parse<IEnumerable<ProductCategory>>(res.Content.ReadAsStringAsync().Result);
			CollectionAssert.AreEquivalent(resList.Select(i => i.Id),
				_unitOfWork.ProductCategoryRepo.Get().Select(i => i.Id));
		}
		[Test]
		public void Create_PassValidObject_AddObject()
		{
			var productCategory = new ProductCategoryDTO()
			{
				Name = "test",
			};
			var res = _httpClient.Post("/productCategory/create", productCategory);

			Assert.AreEqual(res.StatusCode, HttpStatusCode.OK);

			Assert.IsTrue(_unitOfWork.ProductCategoryRepo.Exists(i => i.Name == productCategory.Name));
		}
		[Test]
		public void Create_PassInvalidObject_ReturnBadRequest()
		{
			var productCategory = new ProductCategoryDTO();
			var res = _httpClient.Post("/productCategory/create", productCategory);

			Assert.AreEqual(res.StatusCode, HttpStatusCode.BadRequest);
			Assert.IsFalse(_unitOfWork.ProductCategoryRepo.Exists(i => i.Name == "test"));
		}
		[Test]
		public void Update_PassValidObject_UpdateObject()
		{
			var productCategory = CreateMockCategoryObj();
			productCategory.Name = "test2";
			var res = _httpClient.Put("/productCategory/update", productCategory);
			var updatedProductCategory = _unitOfWork.ProductCategoryRepo.Get(productCategory.Id);

			Assert.AreEqual(res.StatusCode, HttpStatusCode.OK);
			Assert.AreEqual(updatedProductCategory.Name, productCategory.Name);
		}
		[Test]
		public void Update_PassInvalidObject_ReturnBadRequest()
		{
			var productCategory = CreateMockCategoryObj();
			productCategory.Name = "test2";
			var res = _httpClient.Put("/productCategory/update", new
			{
				Id=productCategory.Id,
			});
			var updatedProductCategory = _unitOfWork.ProductCategoryRepo.Get(productCategory.Id);

			Assert.AreEqual(res.StatusCode, HttpStatusCode.BadRequest);
			Assert.AreNotEqual(updatedProductCategory.Name, productCategory.Name);
		}
		[Test]
		public void Delete_PassValidId_DeleteRecord()
		{
			var productCategory = CreateMockCategoryObj();
			var res = _httpClient.Delete($"/productCategory/delete/{productCategory.Id}");
			Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
			Assert.IsNull(_unitOfWork.ProductCategoryRepo.Get(productCategory.Id));
		}
		[Test]
		public void Delete_PassInvalidId_ReturnNotFound()
		{
			var fakeId = Guid.NewGuid().ToString();
			var res = _httpClient.Delete($"/productCategory/delete/{fakeId}");
			Assert.AreEqual(HttpStatusCode.NotFound, res.StatusCode);
		}
		#region HelperMethods
		private ProductCategory CreateMockCategoryObj()
		{
			var productCateogry = new ProductCategory()
			{
				Name = "testCategory",
				IsActive = true,
			};
			_unitOfWork.ProductCategoryRepo.Add(productCateogry);
			return productCateogry;
		}

		#endregion
	}
}
