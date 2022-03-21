using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Product.Application.Exceptions;
using Product.Infrastructure.Persist;
using Product.Infrastructure.Persist.DAOs;
using Product.Infrastructure.Persist.Mappings;
using Product.Infrastructure.Repositories;
using Product.Infrastructure.Tests.Utils;
using Services.Shared.Contracts;
using Services.Shared.Tests;
using System;
using System.Linq;

namespace Product.Infrastructure.Tests.Unit.Repositories
{
	[TestFixture]
	public class ProductRepoTests
	{
		private ProductRepo _productRepo;
		private ICustomMapper _mapper;
		private ApplicationDbContext _db;
		[SetUp]
		public void Setup()
		{
			_db?.Dispose();
			_db = MockActions.MockDbContext("TestDb");
			_mapper = TestUtilsExtension.CreateMapper(new PersistMapperProfile());
			_productRepo = new ProductRepo(_db, _mapper);
		}
		[Test]
		public void Add_PasValidEntity_CreateEntity()
		{
			var product = new Domain.Entities.Product()
			{
				Name = "Test",
				CategoryId = CreateMockCategoryObj().Id,
				CreatedDateTime = DateTime.Now,
				ShortDesc = "no desc",
				IsActive = true,
				Description = "no desc",
				UnitPrice = 45000,
				MainImagePath = "no image"
			};
			_productRepo.Add(product);
			_db.SaveChanges();
			Assert.AreEqual(_db.Products.Count(i => i.Id == product.Id), 1);
		}
		[Test]
		public void Add_PasInvalidEntity_ThrowException()
		{
			var product = new Domain.Entities.Product()
			{
				Name = "Test",
				IsActive = true,
				MainImagePath = "no image"
			};
			Assert.Throws<InsertOperationException>(() =>
			{
				_productRepo.Add(product);
			});
			Assert.AreEqual(_db.Products.Count(), 0);
		}
		[Test]
		public void Update_PasValidEntity_UpdateEntity()
		{
			var product = CreateMockObj();
			_db.Entry(product).State = EntityState.Detached;
			product.Name = "updatedTest";
			_productRepo.Update(_mapper.Map<Domain.Entities.Product>(product));

			var updatedProduct = _db.Products.Find(product.Id);
			Assert.AreEqual(updatedProduct?.Name, product.Name);
		}
		[Test]
		public void Update_PasInvalidEntity_ThrowException()
		{
			var product = CreateMockObj();
			var invalidProduct = new Domain.Entities.Product()
			{
				Id = product.Id,
				Name = "updatedTest"
			};
			Assert.Throws<UpdateOperationException>(() =>
				{
					_productRepo.Update(invalidProduct);
				});
			Assert.AreNotEqual(invalidProduct.Name, product.Name);
		}
		#region HelperMethods
		private ProductCategoryDAO CreateMockCategoryObj()
		{
			var productCateogry = new ProductCategoryDAO()
			{
				Name = "testCategory",
				IsActive = true,
			};
			var result = _db.ProductCategories.Add(productCateogry);
			productCateogry.Id = result.Entity.Id;
			_db.SaveChanges();
			return productCateogry;
		}
		private ProductDAO CreateMockObj()
		{
			var product = new ProductDAO()
			{
				Name = "test",
				CategoryId = CreateMockCategoryObj().Id,
				CreatedDateTime = DateTime.Now,
				Description = "no desc",
				ShortDesc = "no description",
				MainImagePath = "no path",
				UnitPrice = 29348
			};
			var result = _db.Products.Add(product);
			product.Id = result.Entity.Id;
			_db.SaveChanges();
			return product;
		}
		#endregion

	}
}
