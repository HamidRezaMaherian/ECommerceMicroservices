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
			var db = MockActions.MockDbContext();
			_unitOfWork = MockActions.MockUnitOfWork
				(db, TestUtilsExtension.CreateMapper(new PersistMapperProfile()));

			var httpClient = new TestingWebAppFactory<Program>(s =>
			{
				var dbContextConfiguration = s.SingleOrDefault(opt => opt.ServiceType == typeof(ApplicationDbContext));
				if (dbContextConfiguration != null)
					s.Remove(dbContextConfiguration);
				s.AddScoped(opt =>
				{
					return db;
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
		public void GetAllForProduct_ReturnAllProductStocks()
		{
			var stock = CreateStock();
			var res = _httpClient.Get<IEnumerable<Stock>>($"/stock/getallForProduct/{stock.ProductId}");

			CollectionAssert.AreEquivalent(res.Select(i => i.Id),
				_unitOfWork.StockRepo.Get().Where(i => i.ProductId == stock.ProductId)
				.Select(i => i.Id));
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
				ProductId = Guid.NewGuid().ToString(),
				Count = 12,
				StoreId = CreateStore().Id
			};
			_unitOfWork.StockRepo.Add(stock);
			return stock;
		}

		#endregion
	}
}
