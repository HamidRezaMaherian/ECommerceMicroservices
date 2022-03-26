using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Product.Application.Exceptions;
using Product.Application.Tools;
using Product.Domain.Entities;
using Product.Infrastructure.Persist;
using Product.Infrastructure.Persist.DAOs;
using Product.Infrastructure.Persist.Mappings;
using Product.Infrastructure.Repositories;
using Product.Infrastructure.Tests.Utils;
using System.Linq;

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
			_db = MockActions.MockDbContext("TestDb");
			_mapper = TestUtilsExtension.CreateMapper(new PersistMapperProfile());
			_brandRepo = new BrandRepo(_db, _mapper);
		}
		[Test]
		public void Add_PasValidEntity_CreateEntity()
		{
			var brand = new Brand()
			{
				Name = "Test",
				ImagePath = "no image",
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
		#region HelperMethods
		private BrandDAO CreateMockObj()
		{
			var brand = new BrandDAO()
			{
				Name = "test",
				IsActive = true
			};
			var result = _db.Brands.Add(brand);
			brand.Id = result.Entity.Id;
			_db.SaveChanges();
			return brand;
		}
		#endregion

	}
}
