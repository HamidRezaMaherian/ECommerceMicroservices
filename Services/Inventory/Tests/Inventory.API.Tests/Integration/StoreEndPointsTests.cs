using Inventory.API.Tests.Utils;
using Inventory.Application.Configurations;
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
using Services.Shared.AppUtils;
using Services.Shared.Contracts;
using Services.Shared.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
namespace Inventory.API.Tests.Integration
{
	[TestFixture]
	public class StoreEndPointsTests
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
			var stores = _unitOfWork.StoreRepo.Get();
			foreach (var item in stores)
			{
				_unitOfWork.StoreRepo.Delete(item);
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

			Assert.AreEqual(HttpStatusCode.OK,res.StatusCode);
			Assert.IsTrue(_unitOfWork.StoreRepo.Exists(i => i.Name == store.Name));
		}
		[Test]
		public void Create_PassInvalidObject_ReturnBadRequest()
		{
			var store = new StoreDTO()
			{
				Name="test",
			};
			var res = _httpClient.Post("/store/create", store);

			Assert.AreEqual(HttpStatusCode.BadRequest,res.StatusCode);
			Assert.IsFalse(_unitOfWork.StoreRepo.Exists(i=> true));
		}
		[Test]
		public void Update_PassValidObject_UpdateObject()
		{
			var store = CreateStore();
			store.Name = "test2";
			var res = _httpClient.Put("/store/update", store);
			var updatedStore = _mapper.Map<StoreDTO>(
				_unitOfWork.StoreRepo.Get(store.Id));

			Assert.AreEqual(HttpStatusCode.OK,res.StatusCode);
			Assert.AreEqual(updatedStore.Name, store.Name);
		}
		[Test]
		public void Update_PassInvalidObject_ReturnBadRequest()
		{
			var store = CreateStore();
			store.Name = "test2";
			var res = _httpClient.Put("/store/update", new
			{
				Id = store.Id,
			});
			var updatedStore = _mapper.Map<StoreDTO>(
				_unitOfWork.StoreRepo.Get(store.Id));

			Assert.AreEqual(HttpStatusCode.BadRequest,res.StatusCode);
			Assert.AreNotEqual(updatedStore.Name, store.Name);
		}
		[Test]
		public void Delete_PassValidId_DeleteRecord()
		{
			var store = CreateStore();
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
		#endregion
	}
}
