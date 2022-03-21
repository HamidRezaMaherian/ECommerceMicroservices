using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Product.Application.Exceptions;
using Product.Domain.Entities;
using Product.Infrastructure.Persist;
using Product.Infrastructure.Persist.DAOs;
using Product.Infrastructure.Persist.Mappings;
using Product.Infrastructure.Repositories;
using Product.Infrastructure.Tests.Utils;
using Services.Shared.Contracts;
using Services.Shared.Tests;
using System.Linq;

namespace Product.Infrastructure.Tests.Unit.Repositories
{
	[TestFixture]
	public class ProductCategoryRepoTests
	{
		private ProductCategoryRepo _productCategoryRepo;
		private ICustomMapper _mapper;
		private ApplicationDbContext _db;
		[SetUp]
		public void Setup()
		{
			_db?.Dispose();
			_db = MockActions.MockDbContext("TestDb");
			_mapper = TestUtilsExtension.CreateMapper(new PersistMapperProfile());
			_productCategoryRepo = new ProductCategoryRepo(_db, _mapper);
		}
		[Test]
		public void Add_PasValidEntity_CreateEntity()
		{
			var product = new ProductCategory()
			{
				Name = "Test",
				IsActive = true,
			};
			_productCategoryRepo.Add(product);
			_db.SaveChanges();
			Assert.AreEqual(_db.ProductCategories.Count(i => i.Id == product.Id), 1);
		}
		[Test]
		public void Add_PasInvalidEntity_ThrowException()
		{
			var product = new ProductCategory();
			Assert.Throws<InsertOperationException>(() =>
			{
				_productCategoryRepo.Add(product);
			});
			Assert.AreEqual(_db.ProductCategories.Count(), 0);
		}
		[Test]
		public void Update_PasValidEntity_UpdateEntity()
		{
			var productCategory = CreateMockObj();
			_db.Entry(productCategory).State = EntityState.Detached;
			productCategory.Name = "updatedTest";
			_productCategoryRepo.Update(_mapper.Map<ProductCategory>(productCategory));

			var updatedProductCategory = _db.ProductCategories.Find(productCategory.Id);
			Assert.AreEqual(updatedProductCategory?.Name, productCategory.Name);
		}
		[Test]
		public void Update_PasInvalidEntity_ThrowException()
		{
			var product = CreateMockObj();
			var invalidProduct = new ProductCategory()
			{
				Id = product.Id,
				Name = "updatedTest"
			};
			Assert.Throws<UpdateOperationException>(() =>
				{
					_productCategoryRepo.Update(invalidProduct);
				});
			Assert.AreNotEqual(invalidProduct.Name, product.Name);
		}
		#region HelperMethods
		private ProductCategoryDAO CreateMockObj()
		{
			var productCategory = new ProductCategoryDAO()
			{
				Name = "test",
			};
			var result = _db.ProductCategories.Add(productCategory);
			productCategory.Id = result.Entity.Id;
			_db.SaveChanges();
			return productCategory;
		}
		#endregion

	}
}
