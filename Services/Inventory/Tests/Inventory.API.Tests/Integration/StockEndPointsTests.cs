using Inventory.API.Tests.Utils;
using Inventory.Application.DTOs;
using Inventory.Application.UnitOfWork;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.Persist;
using Inventory.Infrastructure.Persist.Mappings;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using NUnit.Framework;
using Services.Shared.APIUtils;
using Services.Shared.AppUtils;
using Services.Shared.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
namespace Inventory.API.Tests.Integration
{
	[TestFixture]
	public class StockEndPointsTests
	{
		private HttpRequestHelper _httpClient;
		private IUnitOfWork _unitOfWork;
		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			var testDB = new Dictionary<string, IList<object>>();
			string dbName = "test_db";
			var dbContext = MockActions.MockDbContext(dbName, testDB);

			_unitOfWork = new UnitOfWork(dbContext,
				TestUtilsExtension.CreateMapper(new PersistMapperProfile()));

			var httpClient = new TestingWebAppFactory<Program>(s =>
			{
				var dbContextConfiguration = s.SingleOrDefault(opt => opt.ServiceType == typeof(MongoClient));
				var applicationDbContext = s.SingleOrDefault(opt => opt.ServiceType == typeof(ApplicationDbContext));
				if (dbContextConfiguration != null)
					s.Remove(dbContextConfiguration);
				s.AddScoped(opt =>
				{
					return dbContext;
				});
			}).CreateClient();
			_httpClient = new HttpRequestHelper(httpClient);
		}
		[TearDown]
		public void TearDown()
		{
			var stocks = _unitOfWork.StockRepo.Get();
			foreach (var item in stocks)
			{
				_unitOfWork.StockRepo.Delete(item);
			}
		}
		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			_httpClient.Dispose();
			_unitOfWork.Dispose();
		}
		[Test]
		public void GetAll_ReturnAllStocks()
		{
			var res = _httpClient.Get("/stock/getall");

			var resList = JsonHelper.Parse<IEnumerable<Stock>>(res.Content.ReadAsStringAsync().Result);

			CollectionAssert.AreEquivalent(resList.Select(i => i.Id),
				_unitOfWork.StockRepo.Get().Select(i => i.Id));
		}
		[Test]
		public void Create_PassValidObject_AddObject()
		{
			var stock = new StockDTO()
			{
				StoreId = CreateStore().Id,
				Count = 12,
				ProductId = Guid.NewGuid().ToString(),
				IsActive = true,
			};
			var res = _httpClient.Post("/stock/create", stock);

			Assert.AreEqual(res.StatusCode, HttpStatusCode.OK);

			Assert.IsTrue(_unitOfWork.StockRepo.Exists(i => i.ProductId == stock.ProductId));
		}
		[Test]
		public void Create_PassInvalidObject_ReturnBadRequest()
		{
			var stock = new StockDTO()
			{
				ProductId = Guid.NewGuid().ToString(),
			};
			var res = _httpClient.Post("/stock/create", stock);

			Assert.AreEqual(res.StatusCode, HttpStatusCode.BadRequest);
			Assert.IsFalse(_unitOfWork.StockRepo.Exists(i => i.ProductId == stock.ProductId));
		}
		[Test]
		public void Update_PassValidObject_UpdateObject()
		{
			var stock = new Stock()
			{
				StoreId = CreateStore().Id,
				Count = 12,
				ProductId = Guid.NewGuid().ToString(),
				IsActive = true,
			};
			_unitOfWork.StockRepo.Add(stock);
			stock.Count = 14;
			var res = _httpClient.Put("/stock/update", stock);
			var updatedStock = _unitOfWork.StockRepo.Get(stock.Id);

			Assert.AreEqual(res.StatusCode, HttpStatusCode.OK);
			Assert.AreEqual(updatedStock.Count, stock.Count);
		}
		[Test]
		public void Update_PassInvalidObject_ReturnBadRequest()
		{
			var stock = new Stock()
			{
				StoreId = CreateStore().Id,
				Count = 12,
				ProductId = Guid.NewGuid().ToString(),
				IsActive = true,
			};
			_unitOfWork.StockRepo.Add(stock);
			stock.Count = 14;
			var res = _httpClient.Put("/stock/update", new
			{
				Id = stock.Id,
			});
			var updatedStock = _unitOfWork.StockRepo.Get(stock.Id);

			Assert.AreEqual(res.StatusCode, HttpStatusCode.BadRequest);
			Assert.AreNotEqual(updatedStock.Count, stock.Count);
		}
		[Test]
		public void Delete_PassValidId_DeleteRecord()
		{
			var stock = new Stock()
			{
				StoreId = CreateStore().Id,
				Count = 12,
				ProductId = Guid.NewGuid().ToString(),
				IsActive = true,
			};
			_unitOfWork.StockRepo.Add(stock);
			var res = _httpClient.Delete($"/stock/delete/{stock.Id}");
			Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
			Assert.IsNull(_unitOfWork.StockRepo.Get(stock.Id));
		}
		[Test]
		public void Delete_PassInvalidId_ReturnNotFound()
		{
			var fakeId = Guid.NewGuid().ToString();
			var res = _httpClient.Delete($"/stock/delete/{fakeId}");
			Assert.AreEqual(HttpStatusCode.NotFound, res.StatusCode);
		}
		#region HelperMethods
		private Store CreateStore()
		{
			var store = new Store()
			{
				Name = "test",
				ShortDesc = "no desc",
				Description = "no desc"
			};
			_unitOfWork.StoreRepo.Add(store);
			return store;
		}

		#endregion
	}
}
