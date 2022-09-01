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
using Services.Shared.AppUtils;
using System;
using System.Linq;
using static Product.Infrastructure.Tests.Utils.TestUtilities;

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
			_db = CreateDbContext("TestDb");
			_mapper = CreateMapper(new PersistMapperProfile(CreateCdnResolver()));
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
		[Test]
		public void Delete_PasValidId_DeleteEntity()
		{
			var productCategory = CreateMockObj();

			_productCategoryRepo.Delete(productCategory.Id);
			_db.SaveChanges();
			Assert.IsFalse(_db.ProductCategories.AsQueryable().Any(i => i.Id == productCategory.Id));
		}
		[Test]
		public void Delete_PasInvalidId_ThrowException()
		{
			Assert.Throws<DeleteOperationException>(() =>
			{
				_productCategoryRepo.Delete(Guid.NewGuid().ToString());
			});
		}
		[Test]
		public void Get_ReturnAllEntities()
		{
			foreach (var item in Enumerable.Range(1, 3))
			{
				CreateMockObj($"title{item}");
			}

			CollectionAssert.AreEquivalent(_db.ProductCategories.AsQueryable().Select(i => i.Id).ToList(),
				_productCategoryRepo.Get().Select(i => i.Id));
		}
		[Test]
		public void Get_PassValidQueryParam_ReturnFilteredEntities()
		{
			foreach (var item in Enumerable.Range(1, 10))
			{
				CreateMockObj($"title{item}");
			}
			QueryParams<ProductCategory> queryParams = new QueryParams<ProductCategory>
			{
				Expression = i => true,
				Skip = 1,
				Take = 5,
			};
			CollectionAssert.AreEquivalent(
				_db.ProductCategories.AsQueryable().Skip(1).Take(5).Select(i => i.Id).ToList(),
				_productCategoryRepo.Get(queryParams).Select(i => i.Id));
		}
		[Test]
		public void Get_PassInvalidQueryParam_ThrowException()
		{
			Assert.Throws<ReadOperationException>(() =>
			{
				_productCategoryRepo.Get(null);
			});
		}

		#region HelperMethods
		private ProductCategoryDAO CreateMockObj()
		{
			var productCategory = MockObj("test");
			var result = _db.ProductCategories.Add(productCategory);
			productCategory.Id = result.Entity.Id;
			_db.SaveChanges();
			return productCategory;
		}
		private ProductCategoryDAO CreateMockObj(string name)
		{
			var productCategory = MockObj(name);
			var result = _db.ProductCategories.Add(productCategory);
			productCategory.Id = result.Entity.Id;
			_db.SaveChanges();
			return productCategory;
		}
		private ProductCategoryDAO MockObj(string name)
		{
			var productCategory = new ProductCategoryDAO()
			{
				Name = name,
			};
			return productCategory;
		}
		#endregion

	}
}
