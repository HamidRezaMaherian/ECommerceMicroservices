using Discount.Application.Exceptions;
using Discount.Application.Services;
using Discount.Domain.Entities;
using Discount.Infrastructure.Persist;
using Discount.Infrastructure.Persist.DAOs;
using Discount.Infrastructure.Persist.Mappings;
using Discount.Infrastructure.Repositories;
using Discount.Infrastructure.Tests.Utils;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Services.Shared.AppUtils;
using System;
using System.Linq;

namespace Discount.Infrastructure.Tests.Unit.Repositories
{
	[TestFixture]
	public class PriceDiscountRepoTests
	{
		private PriceDiscountRepo _priceDiscountRepo;
		private ICustomMapper _mapper;
		private ApplicationDbContext _db;
		[SetUp]
		public void Setup()
		{
			_db?.Dispose();
			_db = MockActions.MockDbContext("TestDb");
			_mapper = TestUtilsExtension.CreateMapper(new PersistMapperProfile());
			_priceDiscountRepo = new PriceDiscountRepo(_db, _mapper);
		}
		[Test]
		public void Add_PasValidEntity_CreateEntity()
		{
			var dpercentDscount = CreateMockObj();
			Assert.AreEqual(_db.PriceDiscounts.Count(i => i.Id == dpercentDscount.Id), 1);
		}
		[Test]
		public void Add_PasInvalidEntity_ThrowException()
		{
			var dpercentDscount = new PriceDiscount()
			{
			};
			Assert.Throws<InsertOperationException>(() =>
			{
				_priceDiscountRepo.Add(dpercentDscount);
			});
			Assert.AreEqual(_db.PriceDiscounts.Count(), 0);
		}
		[Test]
		public void Update_PasValidEntity_UpdateEntity()
		{
			var dpercentDscount = CreateMockObj();
			_db.Entry(dpercentDscount).State = EntityState.Detached;
			dpercentDscount.Price += 10;
			_priceDiscountRepo.Update(_mapper.Map<PriceDiscount>(dpercentDscount));

			var updatedDiscount = _db.PriceDiscounts.Find(dpercentDscount.Id);
			Assert.AreEqual(updatedDiscount?.Price, dpercentDscount.Price);
		}
		[Test]
		public void Update_PasInvalidEntity_ThrowException()
		{
			var dpercentDscount = CreateMockObj();
			var invalidDiscount = new PriceDiscount()
			{
				Id = dpercentDscount.Id,
				Price = dpercentDscount.Price + 5
			};
			Assert.Throws<UpdateOperationException>(() =>
				{
					_priceDiscountRepo.Update(invalidDiscount);
				});
			Assert.AreNotEqual(invalidDiscount.Price, dpercentDscount.Price);
		}

		[Test]
		public void Delete_PasValidId_DeleteEntity()
		{
			var slider = CreateMockObj();

			_priceDiscountRepo.Delete(slider.Id);
			_db.SaveChanges();
			Assert.IsFalse(_db.PriceDiscounts.AsQueryable().Any(i => i.Id == slider.Id));
		}
		[Test]
		public void Delete_PasInvalidId_ThrowException()
		{
			Assert.Throws<DeleteOperationException>(() =>
			{
				_priceDiscountRepo.Delete(Guid.NewGuid().ToString());
			});
		}
		[Test]
		public void Get_ReturnAllEntities()
		{
			foreach (var item in Enumerable.Range(1, 3))
			{
				CreateMockObj(item);
			}

			CollectionAssert.AreEquivalent(_db.PriceDiscounts.AsQueryable().Select(i => i.Id).ToList(),
				_priceDiscountRepo.Get().Select(i => i.Id));
		}
		[Test]
		public void Get_PassValidQueryParam_ReturnFilteredEntities()
		{
			foreach (var item in Enumerable.Range(1, 10))
			{
				CreateMockObj(item);
			}
			QueryParams<PriceDiscount> queryParams = new QueryParams<PriceDiscount>
			{
				Expression = i => true,
				Skip = 1,
				Take = 5,
			};
			CollectionAssert.AreEquivalent(
				_db.PriceDiscounts.AsQueryable().Skip(1).Take(5).Select(i => i.Id).ToList(),
				_priceDiscountRepo.Get(queryParams).Select(i => i.Id));
		}
		[Test]
		public void Get_PassInvalidQueryParam_ThrowException()
		{
			Assert.Throws<ReadOperationException>(() =>
			{
				_priceDiscountRepo.Get(null);
			});
		}

		#region HelperMethods
		private PriceDiscountDAO CreateMockObj()
		{
			var dpercentDscount = MockObj(40);
			var result = _db.PriceDiscounts.Add(dpercentDscount);
			dpercentDscount.Id = result.Entity.Id;
			_db.SaveChanges();
			return dpercentDscount;
		}
		private PriceDiscountDAO CreateMockObj(int percent)
		{
			var dpercentDscount = MockObj(percent);
			var result = _db.PriceDiscounts.Add(dpercentDscount);
			dpercentDscount.Id = result.Entity.Id;
			_db.SaveChanges();
			return dpercentDscount;
		}
		private PriceDiscountDAO MockObj(int percent)
		{
			var dpercentDscount = new PriceDiscountDAO()
			{
				StartDateTime = DateTime.Now,
				EndDateTime = DateTime.Now.AddMinutes(1),
				ProductId = Guid.NewGuid().ToString(),
				StoreId = Guid.NewGuid().ToString(),
				Price = 20,
				IsActive = true
			};
			return dpercentDscount;
		}
		#endregion

	}
}
