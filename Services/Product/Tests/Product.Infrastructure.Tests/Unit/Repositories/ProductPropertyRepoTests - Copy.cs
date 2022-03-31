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

namespace Product.Infrastructure.Tests.Unit.Repositories
{
	[TestFixture]
	public class ProductPropertyRepoTests
	{
		private ProductPropertyRepo _productPropertyRepo;
		private ICustomMapper _mapper;
		private ApplicationDbContext _db;
		[SetUp]
		public void Setup()
		{
			_db?.Dispose();
			_db = MockActions.MockDbContext("TestDb");
			_mapper = TestUtilsExtension.CreateMapper(new PersistMapperProfile());
			_productPropertyRepo = new ProductPropertyRepo(_db, _mapper);
		}
		[Test]
		public void Add_PasValidEntity_CreateEntity()
		{
			var productProperty = new ProductProperty()
			{

			};
			_productPropertyRepo.Add(productProperty);
			_db.SaveChanges();
			Assert.AreEqual(_db.ProductProperties.Count(i => i.Id == productProperty.Id), 1);
		}
		[Test]
		public void Add_PasInvalidEntity_ThrowException()
		{
			var productProperty = new ProductProperty()
			{
			};
			Assert.Throws<InsertOperationException>(() =>
			{
				_productPropertyRepo.Add(productProperty);
			});
			Assert.AreEqual(_db.Products.Count(), 0);
		}
		[Test]
		public void Update_PasValidEntity_UpdateEntity()
		{
			var productProperty = CreateMockObj();
			_db.Entry(productProperty).State = EntityState.Detached;
			productProperty.Value = "updatedTest";
			_productPropertyRepo.Update(_mapper.Map<ProductProperty>(productProperty));

			var updatedProduct = _db.ProductProperties.Find(productProperty.Id);
			Assert.AreEqual(updatedProduct?.Value, productProperty.Value);
		}
		[Test]
		public void Update_PasInvalidEntity_ThrowException()
		{
			var productProperty = CreateMockObj();
			var invalidProduct = new ProductProperty()
			{
				Id = productProperty.Id,
				Value = "updatedTest"
			};
			Assert.Throws<UpdateOperationException>(() =>
				{
					_productPropertyRepo.Update(invalidProduct);
				});
			Assert.AreNotEqual(invalidProduct.Value, productProperty.Value);
		}

		[Test]
		public void Delete_PasValidId_DeleteEntity()
		{
			var slider = CreateMockObj();

			_productPropertyRepo.Delete(slider.Id);
			_db.SaveChanges();
			Assert.IsFalse(_db.ProductProperties.AsQueryable().Any(i => i.Id == slider.Id));
		}
		[Test]
		public void Delete_PasInvalidId_ThrowException()
		{
			Assert.Throws<DeleteOperationException>(() =>
			{
				_productPropertyRepo.Delete(Guid.NewGuid().ToString());
			});
		}
		[Test]
		public void Get_ReturnAllEntities()
		{
			foreach (var item in Enumerable.Range(1, 3))
			{
				CreateMockObj($"title{item}");
			}

			CollectionAssert.AreEquivalent(_db.ProductProperties.AsQueryable().Select(i => i.Id).ToList(),
				_productPropertyRepo.Get().Select(i => i.Id));
		}
		[Test]
		public void Get_PassValidQueryParam_ReturnFilteredEntities()
		{
			foreach (var item in Enumerable.Range(1, 10))
			{
				CreateMockObj($"title{item}");
			}
			QueryParams<ProductProperty> queryParams = new QueryParams<ProductProperty>
			{
				Expression = i => true,
				Skip = 1,
				Take = 5,
			};
			CollectionAssert.AreEquivalent(
				_db.ProductProperties.AsQueryable().Skip(1).Take(5).Select(i => i.Id).ToList(),
				_productPropertyRepo.Get(queryParams).Select(i => i.Id));
		}
		[Test]
		public void Get_PassInvalidQueryParam_ThrowException()
		{
			Assert.Throws<ReadOperationException>(() =>
			{
				_productPropertyRepo.Get(null);
			});
		}

		#region HelperMethods
		private PropertyDAO CreateMockProperty()
		{
			var property = new PropertyDAO()
			{
				Name = "test",
				Type = Domain.Enums.PropertyType.String
			};
			_db.Properties.Add(property);
			_db.SaveChanges();
			return property;
		}
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
		private ProductDAO CreateMockProduct()
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
			_db.Products.Add(product);
			_db.SaveChanges();
			return product;
		}
		private ProductPropertyDAO CreateMockObj()
		{
			var productProperty = MockObj("test");
			var result = _db.ProductProperties.Add(productProperty);
			productProperty.Id = result.Entity.Id;
			_db.SaveChanges();
			return productProperty;
		}
		private ProductPropertyDAO CreateMockObj(string name)
		{
			var productProperty = MockObj(name);
			var result = _db.ProductProperties.Add(productProperty);
			productProperty.Id = result.Entity.Id;
			_db.SaveChanges();
			return productProperty;
		}
		private ProductPropertyDAO MockObj(string value)
		{
			var productProperty = new ProductPropertyDAO()
			{
				ProductId=CreateMockProduct().Id,
				PropertyId=CreateMockProperty().Id,
				Value = value,
				IsActive = true
			};
			return productProperty;
		}
		#endregion

	}
}
