using Inventory.Application.Exceptions;
using Inventory.Application.Tools;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.Persist;
using Inventory.Infrastructure.Persist.DAOs;
using Inventory.Infrastructure.Persist.Mappings;
using Inventory.Infrastructure.Repositories;
using Inventory.Infrastructure.Tests.Utils;
using MongoDB.Driver;
using NUnit.Framework;
using Services.Shared.AppUtils;
using System;
using System.Linq;

namespace Inventory.Infrastructure.Tests.Unit.Repositories
{
	[TestFixture]
	public class StockRepoTests
	{
		private StockRepo _stockRepo;
		private ICustomMapper _mapper;
		private ApplicationDbContext _db;
		private Mongo2Go.MongoDbRunner _mongoStarter;
		[SetUp]
		public void Setup()
		{
			_mongoStarter = Mongo2Go.MongoDbRunner.StartForDebugging();
			_db = MockActions.MockDbContext(_mongoStarter);
			_mapper = TestUtilsExtension.CreateMapper(new PersistMapperProfile());
			_stockRepo = new StockRepo(_db, _mapper);
		}
		[TearDown]
		public void TearDown()
		{
			_db?.Dispose();
		}
		[Test]
		public void Add_PasValidEntity_CreateEntity()
		{
			var stock = MockObj("test");
			_stockRepo.Add(_mapper.Map<Stock>(stock));
			Assert.AreEqual(_db.Stocks.CountDocuments(i => i.Id == stock.Id), 1);
		}
		[Test]
		public void Add_PasInvalidEntity_ThrowException()
		{
			var stock = new Stock();
			_stockRepo.Add(stock);
			Assert.AreEqual(_db.Stocks.CountDocuments(i => true), 0);
		}
		[Test]
		public void Update_PasValidEntity_UpdateEntity()
		{
			var stock = CreateMockObj();
			stock.Count += 5;
			_stockRepo.Update(_mapper.Map<Stock>(stock));

			var updatedStock = _db.Stocks.AsQueryable().FirstOrDefault(i => i.Id == stock.Id);
			Assert.AreEqual(updatedStock?.Count, stock.Count);
		}
		[Test]
		public void Update_PasInvalidEntity_ThrowException()
		{
			var stock = CreateMockObj();
			stock.Count += 5;
			var invalidStock = new Stock()
			{
				Id = stock.Id,
				Count = stock.Count
			};
			Assert.Throws<UpdateOperationException>(() =>
			{
				_stockRepo.Update(invalidStock);
			});
			Assert.AreNotEqual(invalidStock.Count, stock.Count);
		}
		[Test]
		public void Delete_PasValidId_DeleteEntity()
		{
			var stock = CreateMockObj();

			_stockRepo.Delete(stock.Id);

			Assert.IsFalse(_db.Stocks.AsQueryable().Any(i => i.Id == stock.Id));
		}
		[Test]
		public void Delete_PasInvalidId_ThrowException()
		{
			var stock = CreateMockObj();
			Assert.Throws<DeleteOperationException>(() =>
			{
				_stockRepo.Delete(stock.Id);
			});
		}
		[Test]
		public void Get_ReturnAllEntities()
		{
			foreach (var item in Enumerable.Range(1, 3))
			{
				CreateMockObj($"title{item}");
			}

			CollectionAssert.AreEquivalent(_db.Stocks.AsQueryable().Select(i => i.Id).ToList(),
				_stockRepo.Get().Select(i => i.Id));
		}
		[Test]
		public void Get_PassValidQueryParam_ReturnFilteredEntities()
		{
			foreach (var item in Enumerable.Range(1, 10))
			{
				CreateMockObj($"title{item}");
			}
			QueryParams<Stock> queryParams = new QueryParams<Stock>
			{
				Expression = i => true,
				Skip = 1,
				Take = 5,
			};
			CollectionAssert.AreEquivalent(
				_db.Stocks.AsQueryable().Skip(1).Take(5).Select(i => i.Id).ToList(),
				_stockRepo.Get(queryParams).Select(i => i.Id));
		}
		[Test]
		public void Get_PassInvalidQueryParam_ThrowException()
		{
			Assert.Throws<ReadOperationException>(() =>
			{
				_stockRepo.Get(null);
			});
		}
		#region HelperMethods
		private StockDAO MockObj(string productId)
		{
			return new StockDAO()
			{
				ProductId = productId,
				Count = 1,
				PropertyId = Guid.NewGuid().ToString(),
				StoreId = CreateMockCategoryObj().Id,
				IsActive = true
			};
		}
		private StoreDAO CreateMockCategoryObj()
		{
			var store = new StoreDAO()
			{
				Id = Guid.NewGuid().ToString(),
				Name = "no name",
				Description = "no desc",
				ShortDesc = "no short desc",
				IsActive = true,
			};
			_db.Stores.InsertOne(store);
			return store;
		}
		private StockDAO CreateMockObj()
		{
			var stock = MockObj(Guid.NewGuid().ToString());
			stock.Id = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 24);
			_db.Stocks.InsertOne(stock);
			return stock;
		}
		private StockDAO CreateMockObj(string title)
		{
			var stock = MockObj(title);
			stock.Id = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 24);
			_db.Stocks.InsertOne(stock);
			return stock;
		}
		#endregion

	}
}
