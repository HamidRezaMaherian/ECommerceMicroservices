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
	public class PercentDiscountRepoTests
	{
		private PercentDiscountRepo _percentDiscountRepo;
		private ICustomMapper _mapper;
		private ApplicationDbContext _db;
		[SetUp]
		public void Setup()
		{
			_db?.Dispose();
			_db = MockActions.MockDbContext("TestDb");
			_mapper = TestUtilsExtension.CreateMapper(new PersistMapperProfile());
			_percentDiscountRepo = new PercentDiscountRepo(_db, _mapper);
		}
		[Test]
		public void Add_PasValidEntity_CreateEntity()
		{
			var dpercentDscount = CreateMockObj();
			Assert.AreEqual(_db.PercentDiscounts.Count(i => i.Id == dpercentDscount.Id), 1);
		}
		[Test]
		public void Add_PasInvalidEntity_ThrowException()
		{
			var dpercentDscount = new PercentDiscount()
			{
			};
			Assert.Throws<InsertOperationException>(() =>
			{
				_percentDiscountRepo.Add(dpercentDscount);
			});
			Assert.AreEqual(_db.PercentDiscounts.Count(), 0);
		}
		[Test]
		public void Update_PasValidEntity_UpdateEntity()
		{
			var dpercentDscount = CreateMockObj();
			_db.Entry(dpercentDscount).State = EntityState.Detached;
			dpercentDscount.Percent += 10;
			_percentDiscountRepo.Update(_mapper.Map<PercentDiscount>(dpercentDscount));

			var updatedDiscount = _db.PercentDiscounts.Find(dpercentDscount.Id);
			Assert.AreEqual(updatedDiscount?.Percent, dpercentDscount.Percent);
		}
		[Test]
		public void Update_PasInvalidEntity_ThrowException()
		{
			var dpercentDscount = CreateMockObj();
			var invalidDiscount = new PercentDiscount()
			{
				Id = dpercentDscount.Id,
				Percent = dpercentDscount.Percent + 5
			};
			Assert.Throws<UpdateOperationException>(() =>
				{
					_percentDiscountRepo.Update(invalidDiscount);
				});
			Assert.AreNotEqual(invalidDiscount.Percent, dpercentDscount.Percent);
		}

		[Test]
		public void Delete_PasValidId_DeleteEntity()
		{
			var slider = CreateMockObj();

			_percentDiscountRepo.Delete(slider.Id);
			_db.SaveChanges();
			Assert.IsFalse(_db.PercentDiscounts.AsQueryable().Any(i => i.Id == slider.Id));
		}
		[Test]
		public void Delete_PasInvalidId_ThrowException()
		{
			Assert.Throws<DeleteOperationException>(() =>
			{
				_percentDiscountRepo.Delete(Guid.NewGuid().ToString());
			});
		}
		[Test]
		public void Get_ReturnAllEntities()
		{
			foreach (var item in Enumerable.Range(1, 3))
			{
				CreateMockObj(item);
			}

			CollectionAssert.AreEquivalent(_db.PercentDiscounts.AsQueryable().Select(i => i.Id).ToList(),
				_percentDiscountRepo.Get().Select(i => i.Id));
		}
		[Test]
		public void Get_PassValidQueryParam_ReturnFilteredEntities()
		{
			foreach (var item in Enumerable.Range(1, 10))
			{
				CreateMockObj(item);
			}
			QueryParams<PercentDiscount> queryParams = new QueryParams<PercentDiscount>
			{
				Expression = i => true,
				Skip = 1,
				Take = 5,
			};
			CollectionAssert.AreEquivalent(
				_db.PercentDiscounts.AsQueryable().Skip(1).Take(5).Select(i => i.Id).ToList(),
				_percentDiscountRepo.Get(queryParams).Select(i => i.Id));
		}
		[Test]
		public void Get_PassInvalidQueryParam_ThrowException()
		{
			Assert.Throws<ReadOperationException>(() =>
			{
				_percentDiscountRepo.Get(null);
			});
		}

		#region HelperMethods
		private PercentDiscountDAO CreateMockObj()
		{
			var dpercentDscount = MockObj(40);
			var result = _db.PercentDiscounts.Add(dpercentDscount);
			dpercentDscount.Id = result.Entity.Id;
			_db.SaveChanges();
			return dpercentDscount;
		}
		private PercentDiscountDAO CreateMockObj(int percent)
		{
			var dpercentDscount = MockObj(percent);
			var result = _db.PercentDiscounts.Add(dpercentDscount);
			dpercentDscount.Id = result.Entity.Id;
			_db.SaveChanges();
			return dpercentDscount;
		}
		private PercentDiscountDAO MockObj(int percent)
		{
			var dpercentDscount = new PercentDiscountDAO()
			{
				StartDateTime = DateTime.Now,
				EndDateTime = DateTime.Now.AddMinutes(1),
				ProductId = Guid.NewGuid().ToString(),
				StoreId = Guid.NewGuid().ToString(),
				Percent = 20,
				IsActive = true
			};
			return dpercentDscount;
		}
		#endregion

	}
}
