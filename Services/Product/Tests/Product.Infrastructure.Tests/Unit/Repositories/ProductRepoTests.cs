using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Product.Application.Exceptions;
using Product.Application.Tools;
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
	public class ProductRepoTests
	{
		private ProductRepo _productRepo;
		private ICustomMapper _mapper;
		private ApplicationDbContext _db;
		[SetUp]
		public void Setup()
		{
			_db = CreateDbContext("TestDb");
			_mapper = CreateMapper(new PersistMapperProfile(CreateCdnResolver()));
			_productRepo = new ProductRepo(_db, _mapper);
		}
		[TearDown]
		public void TearDown()
		{
			_db?.Dispose();
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
				MainImage = new Blob("","","")
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
				MainImage = new Blob("", "", "")
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
		[Test]
		public void Delete_PasValidId_DeleteEntity()
		{
			var product = CreateMockObj();

			_productRepo.Delete(product.Id);
			_db.SaveChanges();
			Assert.IsFalse(_db.Brands.AsQueryable().Any(i => i.Id == product.Id));
		}
		[Test]
		public void Delete_PasInvalidId_ThrowException()
		{
			Assert.Throws<DeleteOperationException>(() =>
			{
				_productRepo.Delete(Guid.NewGuid().ToString());
			});
		}
		[Test]
		public void Get_ReturnAllEntities()
		{
			foreach (var item in Enumerable.Range(1, 3))
			{
				CreateMockObj($"title{item}");
			}

			CollectionAssert.AreEquivalent(_db.Products.AsQueryable().Select(i => i.Id).ToList(),
				_productRepo.Get().Select(i => i.Id));
		}
		[Test]
		public void Get_PassValidQueryParam_ReturnFilteredEntities()
		{
			foreach (var item in Enumerable.Range(1, 10))
			{
				CreateMockObj($"title{item}");
			}
			QueryParams<Domain.Entities.Product> queryParams = new QueryParams<Domain.Entities.Product>
			{
				Expression = i => true,
				Skip = 1,
				Take = 5,
			};
			CollectionAssert.AreEquivalent(
				_db.Products.AsQueryable().Skip(1).Take(5).Select(i => i.Id).ToList(),
				_productRepo.Get(queryParams).Select(i => i.Id));
		}
		[Test]
		public void Get_PassInvalidQueryParam_ThrowException()
		{
			Assert.Throws<ReadOperationException>(() =>
			{
				_productRepo.Get(null);
			});
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
			var product = MockObj("test");
			var result = _db.Products.Add(product);
			product.Id = result.Entity.Id;
			_db.SaveChanges();
			return product;
		}
		private ProductDAO CreateMockObj(string name)
		{
			var product = MockObj(name);
			var result = _db.Products.Add(product);
			product.Id = result.Entity.Id;
			_db.SaveChanges();
			return product;
		}
		private ProductDAO MockObj(string name)
		{
			var product = new ProductDAO()
			{
				Name = name,
				CategoryId = CreateMockCategoryObj().Id,
				CreatedDateTime = DateTime.Now,
				Description = "no desc",
				ShortDesc = "no description",
				MainImagePath = "no path",
				UnitPrice = 29348
			};
			return product;
		}
		#endregion
	}
}
