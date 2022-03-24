using Inventory.API.Tests.Utils;
using Inventory.Application.DTOs;
using Inventory.Application.UnitOfWork;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.Persist;
using Inventory.Infrastructure.Persist.Mappings;
using Microsoft.Extensions.DependencyInjection;
using Mongo2Go;
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
		private MongoDbRunner _mongoDbRunner;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_mongoDbRunner = MongoDbRunner.Start();
			var db = MockActions.MockDbContext(_mongoDbRunner);
			_unitOfWork = MockActions.MockUnitOfWork
			(db, TestUtilsExtension.CreateMapper(new PersistMapperProfile()));

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
		public void GetAll_ReturnAllStores()
		{
			var res = _httpClient.Get<IEnumerable<Store>>("/store/getall");

			CollectionAssert.AreEquivalent(res.Select(i => i.Id),
				_unitOfWork.StoreRepo.Get().Select(i => i.Id));
		}
		[Test]
		public void Create_PassValidObject_AddObject()
		{
			var store = new StoreDTO()
			{
				Name = "test",
				Description = "no desc",
				ShortDesc = "no desc"
			};
			var res = _httpClient.Post("/store/create", store);

			Assert.AreEqual(res.StatusCode, HttpStatusCode.OK);

			Assert.IsTrue(_unitOfWork.StoreRepo.Exists(i => i.Name == store.Name));
		}
		[Test]
		public void Create_PassInvalidObject_ReturnBadRequest()
		{
			var store = new StoreDTO();
			var res = _httpClient.Post("/store/create", store);

			Assert.AreEqual(res.StatusCode, HttpStatusCode.BadRequest);
			Assert.IsFalse(_unitOfWork.StoreRepo.Exists(i => i.Name == "test"));
		}
		[Test]
		public void Update_PassValidObject_UpdateObject()
		{
			var store = new Store()
			{
				Name = "test",
				Description = "no desc",
				ShortDesc = "no desc"
			};
			_unitOfWork.StoreRepo.Add(store);
			store.Name = "test2";
			var res = _httpClient.Put("/store/update", store);
			var updatedStore = _unitOfWork.StoreRepo.Get(store.Id);

			Assert.AreEqual(res.StatusCode, HttpStatusCode.OK);
			Assert.AreEqual(updatedStore.Name, store.Name);
		}
		[Test]
		public void Update_PassInvalidObject_ReturnBadRequest()
		{
			var store = new Store()
			{
				Name = "test",
				Description = "no desc",
				ShortDesc = "no desc",
				IsActive = true,
			};
			_unitOfWork.StoreRepo.Add(store);
			store.Name = "test2";
			var res = _httpClient.Put("/store/update", new
			{
				Id = store.Id,
			});
			var updatedStore = _unitOfWork.StoreRepo.Get(store.Id);

			Assert.AreEqual(res.StatusCode, HttpStatusCode.BadRequest);
			Assert.AreNotEqual(updatedStore.Name, store.Name);
		}
		[Test]
		public void Delete_PassValidId_DeleteRecord()
		{
			var store = new Store()
			{
				Name = "test",
			};
			_unitOfWork.StoreRepo.Add(store);
			var res = _httpClient.Delete($"/store/delete/{store.Id}");
			Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
			Assert.IsNull(_unitOfWork.StoreRepo.Get(store.Id));
		}
		[Test]
		public void Delete_PassInvalidId_ReturnNotFound()
		{
			var fakeId = Guid.NewGuid().ToString();
			var res = _httpClient.Delete($"/store/delete/{fakeId}");
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
