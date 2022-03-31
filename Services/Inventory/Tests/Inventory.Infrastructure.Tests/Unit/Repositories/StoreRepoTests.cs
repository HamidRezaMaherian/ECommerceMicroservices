using MongoDB.Driver;
using NUnit.Framework;
using Services.Shared.AppUtils;
using System;
using System.Linq;
using Inventory.Application.Exceptions;
using Inventory.Application.Tools;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.Persist;
using Inventory.Infrastructure.Persist.DAOs;
using Inventory.Infrastructure.Persist.Mappings;
using Inventory.Infrastructure.Repositories;
using Inventory.Infrastructure.Tests.Utils;

namespace Inventory.Infrastructure.Tests.Unit.Repositories
{
	[TestFixture]
	public class StoreRepoTests
	{
		private StoreRepo _storeRepo;
		private ICustomMapper _mapper;
		private ApplicationDbContext _db;
		private Mongo2Go.MongoDbRunner _mongoStarter;
		[SetUp]
		public void Setup()
		{
			_mongoStarter = Mongo2Go.MongoDbRunner.StartForDebugging();
			_db = MockActions.MockDbContext(_mongoStarter);
			_mapper = TestUtilsExtension.CreateMapper(new PersistMapperProfile());
			_storeRepo = new StoreRepo(_db, _mapper);
		}
		[TearDown]
		public void TearDown()
		{
			_db?.Dispose();
		}
		[Test]
		public void Add_PasValidEntity_CreateEntity()
		{
			var slider = MockObj("test");
			_storeRepo.Add(_mapper.Map<Store>(slider));
			Assert.AreEqual(_db.Stores.CountDocuments(i => i.Id == slider.Id), 1);
		}
		[Test]
		public void Add_PasInvalidEntity_ThrowException()
		{
			var slider = new Store();
			_storeRepo.Add(slider);
			Assert.AreEqual(_db.Stores.CountDocuments(i => true), 0);
		}
		[Test]
		public void Update_PasValidEntity_UpdateEntity()
		{
			var slider = CreateMockObj();
			slider.Name = "updatedTest";
			_storeRepo.Update(_mapper.Map<Store>(slider));

			var updatedStore = _db.Stores.AsQueryable().FirstOrDefault(i => i.Id == slider.Id);
			Assert.AreEqual(updatedStore?.Name, slider.Name);
		}
		[Test]
		public void Update_PasInvalidEntity_ThrowException()
		{
			var slider = CreateMockObj();
			var invalidStore = new Store()
			{
				Id = slider.Id,
				Name = "updatedTest"
			};
			Assert.Throws<UpdateOperationException>(() =>
			{
				_storeRepo.Update(invalidStore);
			});
			Assert.AreNotEqual(invalidStore.Name, slider.Name);
		}
		[Test]
		public void Delete_PasValidId_DeleteEntity()
		{
			var slider = CreateMockObj();

			_storeRepo.Delete(slider.Id);

			Assert.IsFalse(_db.Stores.AsQueryable().Any(i => i.Id == slider.Id));
		}
		[Test]
		public void Delete_PasInvalidId_ThrowException()
		{
			var slider = CreateMockObj();
			Assert.Throws<DeleteOperationException>(() =>
			{
				_storeRepo.Delete(slider.Id);
			});
		}
		[Test]
		public void Get_ReturnAllEntities()
		{
			foreach (var item in Enumerable.Range(1, 3))
			{
				CreateMockObj($"title{item}");
			}

			CollectionAssert.AreEquivalent(_db.Stores.AsQueryable().Select(i => i.Id).ToList(),
				_storeRepo.Get().Select(i => i.Id));
		}
		[Test]
		public void Get_PassValidQueryParam_ReturnFilteredEntities()
		{
			foreach (var item in Enumerable.Range(1, 10))
			{
				CreateMockObj($"title{item}");
			}
			QueryParams<Store> queryParams = new QueryParams<Store>
			{
				Expression = i => true,
				Skip = 1,
				Take = 5,
			};
			CollectionAssert.AreEquivalent(
				_db.Stores.AsQueryable().Skip(1).Take(5).Select(i => i.Id).ToList(),
				_storeRepo.Get(queryParams).Select(i => i.Id));
		}
		[Test]
		public void Get_PassInvalidQueryParam_ThrowException()
		{
			Assert.Throws<ReadOperationException>(() =>
			{
				_storeRepo.Get(null);
			});
		}
		#region HelperMethods
		private StoreDAO MockObj(string title)
		{
			return new StoreDAO()
			{
				Name = title,
				Description = "no desc",
				ShortDesc = "no desc",
				IsActive = true
			};
		}
		private StoreDAO CreateMockObj()
		{
			var slider = MockObj("title");
			slider.Id = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 24);
			_db.Stores.InsertOne(slider);
			return slider;
		}
		private StoreDAO CreateMockObj(string title)
		{
			var slider = MockObj(title);
			slider.Id = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 24);
			_db.Stores.InsertOne(slider);
			return slider;
		}
		#endregion

	}
}
