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
	public class PropertyRepoTests
	{
		private PropertyRepo _propertyRepo;
		private ICustomMapper _mapper;
		private ApplicationDbContext _db;
		[SetUp]
		public void Setup()
		{
			_db?.Dispose();
			_db = MockActions.MockDbContext("TestDb");
			_mapper = TestUtilsExtension.CreateMapper(new PersistMapperProfile());
			_propertyRepo = new PropertyRepo(_db, _mapper);
		}
		[Test]
		public void Add_PasValidEntity_CreateEntity()
		{
			var property = new Property()
			{
				Name = "Test",
				Type=Domain.Enums.PropertyType.String
			};
			_propertyRepo.Add(property);
			_db.SaveChanges();
			Assert.AreEqual(_db.Properties.Count(i => i.Id == property.Id), 1);
		}
		[Test]
		public void Add_PasInvalidEntity_ThrowException()
		{
			var property = new Property()
			{
			};
			Assert.Throws<InsertOperationException>(() =>
			{
				_propertyRepo.Add(property);
			});
			Assert.AreEqual(_db.Products.Count(), 0);
		}
		[Test]
		public void Update_PasValidEntity_UpdateEntity()
		{
			var property = CreateMockObj();
			_db.Entry(property).State = EntityState.Detached;
			property.Name = "updatedTest";
			_propertyRepo.Update(_mapper.Map<Property>(property));

			var updatedProduct = _db.Properties.Find(property.Id);
			Assert.AreEqual(updatedProduct?.Name, property.Name);
		}
		[Test]
		public void Update_PasInvalidEntity_ThrowException()
		{
			var property = CreateMockObj();
			var invalidProduct = new Property()
			{
				Id = property.Id,
				Name = "updatedTest"
			};
			Assert.Throws<UpdateOperationException>(() =>
				{
					_propertyRepo.Update(invalidProduct);
				});
			Assert.AreNotEqual(invalidProduct.Name, property.Name);
		}
		[Test]
		public void Delete_PasValidId_DeleteEntity()
		{
			var property = CreateMockObj();

			_propertyRepo.Delete(property.Id);
			_db.SaveChanges();
			Assert.IsFalse(_db.Properties.AsQueryable().Any(i => i.Id == property.Id));
		}
		[Test]
		public void Delete_PasInvalidId_ThrowException()
		{
			Assert.Throws<DeleteOperationException>(() =>
			{
				_propertyRepo.Delete(Guid.NewGuid().ToString());
			});
		}
		[Test]
		public void Get_ReturnAllEntities()
		{
			foreach (var item in Enumerable.Range(1, 3))
			{
				CreateMockObj($"title{item}");
			}

			CollectionAssert.AreEquivalent(_db.Properties.AsQueryable().Select(i => i.Id).ToList(),
				_propertyRepo.Get().Select(i => i.Id));
		}
		[Test]
		public void Get_PassValidQueryParam_ReturnFilteredEntities()
		{
			foreach (var item in Enumerable.Range(1, 10))
			{
				CreateMockObj($"title{item}");
			}
			QueryParams<Property> queryParams = new QueryParams<Property>
			{
				Expression = i => true,
				Skip = 1,
				Take = 5,
			};
			CollectionAssert.AreEquivalent(
				_db.Properties.AsQueryable().Skip(1).Take(5).Select(i => i.Id).ToList(),
				_propertyRepo.Get(queryParams).Select(i => i.Id));
		}
		[Test]
		public void Get_PassInvalidQueryParam_ThrowException()
		{
			Assert.Throws<ReadOperationException>(() =>
			{
				_propertyRepo.Get(null);
			});
		}

		#region HelperMethods
		private PropertyDAO CreateMockObj()
		{
			var property = MockObj("test");
			var result = _db.Properties.Add(property);
			property.Id = result.Entity.Id;
			_db.SaveChanges();
			return property;
		}
		private PropertyDAO CreateMockObj(string name)
		{
			var property = MockObj(name);
			var result = _db.Properties.Add(property);
			property.Id = result.Entity.Id;
			_db.SaveChanges();
			return property;
		}
		private PropertyDAO MockObj(string name)
		{
			var property = new PropertyDAO()
			{
				Name = name,
				Type=Domain.Enums.PropertyType.Number,
				IsActive = true
			};
			return property;
		}
		#endregion

	}
}
