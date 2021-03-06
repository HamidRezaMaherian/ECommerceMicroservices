using Inventory.API.Tests.Utils;
using Inventory.Application.Configurations;
using Inventory.Application.DTOs;
using Inventory.Application.Tools;
using Inventory.Application.UnitOfWork;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.Persist;
using Inventory.Infrastructure.Persist.Mappings;
using Microsoft.Extensions.DependencyInjection;
using Mongo2Go;
using MongoDB.Driver;
using NUnit.Framework;
using Services.Shared.APIUtils;
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
		private MongoDbRunner _mongoDbRunner;
		private ICustomMapper _mapper;
		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_mongoDbRunner = MongoDbRunner.Start();
			var db = MockActions.MockDbContext(_mongoDbRunner);
			_mapper = TestUtilsExtension.CreateMapper(new PersistMapperProfile(), new ServiceMapper());
			_unitOfWork = MockActions.MockUnitOfWork(db, _mapper);

			var httpClient = new TestingWebAppFactory<Program>(s =>
			{
				var dbContextConfiguration = s.SingleOrDefault(opt => opt.ServiceType == typeof(ApplicationDbContext));
				if (dbContextConfiguration != null)
					s.Remove(dbContextConfiguration);
				s.AddScoped(opt =>
				{
					return MockActions.MockDbContext(_mongoDbRunner);
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
		public void HealthCheck_IsOk()
		{
			var res = _httpClient.Get("/health");
			res.EnsureSuccessStatusCode();
		}
		[Test]
		public void GetAllForProduct_ReturnAllStocksForProduct()
		{
			var stock = CreateStock();
			var res = _httpClient.Get<IEnumerable<Stock>>($"/stock/getallforProduct/{stock.ProductId}");

			CollectionAssert.AreEquivalent(res.Select(i => i.Id),
				_unitOfWork.StockRepo.Get().Select(i => i.Id));
		}
		[Test]
		public void GetAllForStore_ReturnAllStocksForStore()
		{
			var stock = CreateStock();
			var res = _httpClient.Get<IEnumerable<Stock>>($"/stock/getallforStore/{stock.StoreId}");

			CollectionAssert.AreEquivalent(res.Select(i => i.Id),
				_unitOfWork.StockRepo.Get().Select(i => i.Id));
		}
		[Test]
		public void Create_PassValidObject_AddObject()
		{
			var stock = new StockDTO()
			{
				StoreId = CreateStore().Id,
				Count = 12,
				IsActive = true,
				ProductId = Guid.NewGuid().ToString()
			};
			var res = _httpClient.Post("/stock/create", stock);

			Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);

			Assert.IsTrue(_unitOfWork.StockRepo.Exists(i => i.StoreId == stock.StoreId));
		}
		[Test]
		public void Create_PassInvalidObject_ReturnBadRequest()
		{
			var stock = new StockDTO()
			{
				StoreId = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 24)
			};
			var res = _httpClient.Post("/stock/create", stock);

			Assert.AreEqual(HttpStatusCode.BadRequest, res.StatusCode);
			Assert.IsFalse(_unitOfWork.StockRepo.Exists(i => i.StoreId == stock.StoreId));
		}
		[Test]
		public void Update_PassValidObject_UpdateObject()
		{
			var stock = CreateStock();
			stock.Count += 3;
			var res = _httpClient.Put("/stock/update", stock);
			var updatedStock = _mapper.Map<StockDTO>(
				_unitOfWork.StockRepo.Get(stock.Id));

			Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
			Assert.AreEqual(updatedStock.Count, stock.Count);
		}
		[Test]
		public void Update_PassInvalidObject_ReturnBadRequest()
		{
			var stock = CreateStock();
			stock.Count += 3;
			var res = _httpClient.Put("/stock/update", new
			{
				Id = stock.Id,
			});
			var updatedStock = _mapper.Map<StockDTO>(
				_unitOfWork.StockRepo.Get(stock.Id));

			Assert.AreEqual(HttpStatusCode.BadRequest, res.StatusCode);
			Assert.AreNotEqual(updatedStock.Count, stock.Count);
		}
		[Test]
		public void Delete_PassValidId_DeleteRecord()
		{
			var stock = CreateStock();
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
				Name = "testStore",
				Description = "no desc",
				IsActive = true,
				ShortDesc = "no desc",
			};
			_unitOfWork.StoreRepo.Add(store);
			return store;
		}

		private Stock CreateStock()
		{
			var stock = new Stock()
			{
				ProductId = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 24),
				Count = 12,
				StoreId = CreateStore().Id
			};
			_unitOfWork.StockRepo.Add(stock);
			return stock;
		}

		#endregion
	}
}
