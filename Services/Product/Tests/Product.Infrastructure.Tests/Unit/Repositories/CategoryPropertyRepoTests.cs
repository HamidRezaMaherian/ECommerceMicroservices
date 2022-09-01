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
	public class CategoryPropertyRepoTests
	{
		private CategoryPropertyRepo _categoryPropertyRepo;
		private ICustomMapper _mapper;
		private ApplicationDbContext _db;
		[SetUp]
		public void Setup()
		{
			_db?.Dispose();
			_db = CreateDbContext("TestDb");
			_mapper = CreateMapper(new PersistMapperProfile(CreateCdnResolver()));
			_categoryPropertyRepo = new CategoryPropertyRepo(_db, _mapper);
		}
		[Test]
		public void Add_PasValidEntity_CreateEntity()
		{
			var categoryProperty = MockObj("test");
			_categoryPropertyRepo.Add(_mapper.Map<CategoryProperty>(categoryProperty));
			_db.SaveChanges();
			Assert.AreEqual(
				_db.CategoryProperties.Count(i =>
				i.PropertyId == categoryProperty.PropertyId &&
				i.CategoryId == categoryProperty.CategoryId),
				1);
		}
		[Test]
		public void Add_PasInvalidEntity_ThrowException()
		{
			var categoryProperty = new CategoryProperty()
			{
			};
			Assert.Throws<InsertOperationException>(() =>
			{
				_categoryPropertyRepo.Add(categoryProperty);
			});
			Assert.AreEqual(_db.Products.Count(), 0);
		}
		[Test]
		public void Update_PasValidEntity_UpdateEntity()
		{
			var categoryProperty = CreateMockObj();
			_db.Entry(categoryProperty).State = EntityState.Detached;
			categoryProperty.PropertyId = CreateMockProperty().Id;
			_categoryPropertyRepo.Update(_mapper.Map<CategoryProperty>(categoryProperty));

			var updatedProduct = _db.CategoryProperties.FirstOrDefault(i => 
			i.PropertyId == categoryProperty.PropertyId &&
			i.CategoryId==categoryProperty.CategoryId);

			Assert.AreEqual(updatedProduct?.PropertyId, categoryProperty.PropertyId);
		}
		[Test]
		public void Update_PasInvalidEntity_ThrowException()
		{
			var categoryProperty = CreateMockObj();
			var invalidProduct = new CategoryProperty()
			{
				CategoryId=categoryProperty.CategoryId
			};
			Assert.Throws<UpdateOperationException>(() =>
				{
					_categoryPropertyRepo.Update(invalidProduct);
				});
			Assert.AreNotEqual(invalidProduct.CategoryId, categoryProperty.CategoryId);
		}

		[Test]
		public void Delete_PasValidId_DeleteEntity()
		{
			var categoryProperty = CreateMockObj();

			_categoryPropertyRepo.Delete(categoryProperty);
			_db.SaveChanges();
			Assert.IsFalse(_db.CategoryProperties.AsQueryable().Any(i => i.PropertyId== categoryProperty.PropertyId));
		}
		[Test]
		public void Delete_PasInvalidId_ThrowException()
		{
			Assert.Throws<DeleteOperationException>(() =>
			{
				_categoryPropertyRepo.Delete(Guid.NewGuid().ToString());
			});
		}
		[Test]
		public void Get_ReturnAllEntities()
		{
			foreach (var item in Enumerable.Range(1, 3))
			{
				CreateMockObj($"title{item}");
			}

			CollectionAssert.AreEquivalent(_db.CategoryProperties.AsQueryable().Select(i => i.PropertyId).ToList(),
				_categoryPropertyRepo.Get().Select(i => i.PropertyId));
		}
		[Test]
		public void Get_PassValidQueryParam_ReturnFilteredEntities()
		{
			foreach (var item in Enumerable.Range(1, 10))
			{
				CreateMockObj($"title{item}");
			}
			QueryParams<CategoryProperty> queryParams = new QueryParams<CategoryProperty>
			{
				Expression = i => true,
				Skip = 1,
				Take = 5,
			};
			CollectionAssert.AreEquivalent(
				_db.CategoryProperties.AsQueryable().Skip(1).Take(5).Select(i => i.PropertyId).ToList(),
				_categoryPropertyRepo.Get(queryParams).Select(i => i.PropertyId));
		}
		[Test]
		public void Get_PassInvalidQueryParam_ThrowException()
		{
			Assert.Throws<ReadOperationException>(() =>
			{
				_categoryPropertyRepo.Get(null);
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
		private CategoryPropertyDAO CreateMockObj()
		{
			var categoryProperty = MockObj("test");
			var result = _db.CategoryProperties.Add(categoryProperty);
			categoryProperty.PropertyId = result.Entity.PropertyId;
			categoryProperty.CategoryId = result.Entity.CategoryId;
			_db.SaveChanges();
			return categoryProperty;
		}
		private CategoryPropertyDAO CreateMockObj(string name)
		{
			var categoryProperty = MockObj(name);
			var result = _db.CategoryProperties.Add(categoryProperty);
			categoryProperty.PropertyId = result.Entity.PropertyId;
			_db.SaveChanges();
			return categoryProperty;
		}
		private CategoryPropertyDAO MockObj(string value)
		{
			var categoryProperty = new CategoryPropertyDAO()
			{
				CategoryId = CreateMockCategoryObj().Id,
				PropertyId = CreateMockProperty().Id,				
				IsActive = true
			};
			return categoryProperty;
		}
		#endregion

	}
}
