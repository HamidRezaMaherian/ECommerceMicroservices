using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Product.Application.Exceptions;
using Product.Application.Tools;
using Product.Domain.Entities;
using Product.Domain.ValueObjects;
using Product.Infrastructure.Persist;
using Product.Infrastructure.Persist.DAOs;
using Product.Infrastructure.Persist.Mappings;
using Product.Infrastructure.Repositories;
using Product.Infrastructure.Tests.Utils;
using Services.Shared.AppUtils;
using System;
using System.Linq;
using static Product.Infrastructure.Tests.Utils.TestUtilities;

namespace Product.Infrastructure.Tests.Unit.Repositories
{
	[TestFixture]
	public class BrandRepoTests
	{
		private BrandRepo _brandRepo;
		private ICustomMapper _mapper;
		private ApplicationDbContext _db;
		[SetUp]
		public void Setup()
		{
			_db?.Dispose();
			_db = CreateDbContext("TestDb");
			_mapper = CreateMapper(new PersistMapperProfile(CreateCdnResolver()));
			_brandRepo = new BrandRepo(_db, _mapper);
		}
		[Test]
		public void Add_PasValidEntity_CreateEntity()
		{
			var brand = new Brand()
			{
				Name = "Test",
				Image =new Blob("","",""),
				IsActive = true,
			};
			_brandRepo.Add(brand);
			_db.SaveChanges();
			Assert.AreEqual(_db.Brands.Count(i => i.Id == brand.Id), 1);
		}
		[Test]
		public void Add_PasInvalidEntity_ThrowException()
		{
			var brand = new Brand()
			{
			};
			Assert.Throws<InsertOperationException>(() =>
			{
				_brandRepo.Add(brand);
			});
			Assert.AreEqual(_db.Products.Count(), 0);
		}
		[Test]
		public void Update_PasValidEntity_UpdateEntity()
		{
			var brand = CreateMockObj();
			_db.Entry(brand).State = EntityState.Detached;
			brand.Name = "updatedTest";
			_brandRepo.Update(_mapper.Map<Brand>(brand));

			var updatedProduct = _db.Brands.Find(brand.Id);
			Assert.AreEqual(updatedProduct?.Name, brand.Name);
		}
		[Test]
		public void Update_PasInvalidEntity_ThrowException()
		{
			var brand = CreateMockObj();
			var invalidProduct = new Brand()
			{
				Id = brand.Id,
				Name = "updatedTest"
			};
			Assert.Throws<UpdateOperationException>(() =>
				{
					_brandRepo.Update(invalidProduct);
				});
			Assert.AreNotEqual(invalidProduct.Name, brand.Name);
		}

		[Test]
		public void Delete_PasValidId_DeleteEntity()
		{
			var slider = CreateMockObj();

			_brandRepo.Delete(slider.Id);
			_db.SaveChanges();
			Assert.IsFalse(_db.Brands.AsQueryable().Any(i => i.Id == slider.Id));
		}
		[Test]
		public void Delete_PasInvalidId_ThrowException()
		{
			Assert.Throws<DeleteOperationException>(() =>
			{
				_brandRepo.Delete(Guid.NewGuid().ToString());
			});
		}
		[Test]
		public void Get_ReturnAllEntities()
		{
			foreach (var item in Enumerable.Range(1, 3))
			{
				CreateMockObj($"title{item}");
			}

			CollectionAssert.AreEquivalent(_db.Brands.AsQueryable().Select(i => i.Id).ToList(),
				_brandRepo.Get().Select(i => i.Id));
		}
		[Test]
		public void Get_PassValidQueryParam_ReturnFilteredEntities()
		{
			foreach (var item in Enumerable.Range(1, 10))
			{
				CreateMockObj($"title{item}");
			}
			QueryParams<Brand> queryParams = new QueryParams<Brand>
			{
				Expression = i => true,
				Skip = 1,
				Take = 5,
			};
			CollectionAssert.AreEquivalent(
				_db.Brands.AsQueryable().Skip(1).Take(5).Select(i => i.Id).ToList(),
				_brandRepo.Get(queryParams).Select(i => i.Id));
		}
		[Test]
		public void Get_PassInvalidQueryParam_ThrowException()
		{
			Assert.Throws<ReadOperationException>(() =>
			{
				_brandRepo.Get(null);
			});
		}

		#region HelperMethods
		private BrandDAO CreateMockObj()
		{
			var brand = MockObj("test");
			var result = _db.Brands.Add(brand);
			brand.Id = result.Entity.Id;
			_db.SaveChanges();
			return brand;
		}
		private BrandDAO CreateMockObj(string name)
		{
			var brand = MockObj(name);
			var result = _db.Brands.Add(brand);
			brand.Id = result.Entity.Id;
			_db.SaveChanges();
			return brand;
		}
		private BrandDAO MockObj(string name)
		{
			var brand = new BrandDAO()
			{
				Name = name,
				IsActive = true
			};
			return brand;
		}
		#endregion

	}
}
