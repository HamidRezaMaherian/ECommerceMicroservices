using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using NUnit.Framework;
using Services.Shared.AppUtils;
using System;
using System.Linq;
using UI.Application.Exceptions;
using UI.Application.Tools;
using UI.Domain.Entities;
using UI.Infrastructure.Persist;
using UI.Infrastructure.Persist.DAOs;
using UI.Infrastructure.Persist.Mappings;
using UI.Infrastructure.Repositories;
using UI.Infrastructure.Tests.Utils;

namespace UI.Infrastructure.Tests.Unit.Repositories
{
	[TestFixture]
	public class FaqCategoryRepoTests
	{
		private FaqCategoryRepo _faqCategoryRepo;
		private ICustomMapper _mapper;
		private ApplicationDbContext _db;
		private Mongo2Go.MongoDbRunner _mongoStarter;
		[SetUp]
		public void Setup()
		{
			_mongoStarter = Mongo2Go.MongoDbRunner.StartForDebugging();
			_db = MockActions.MockDbContext(_mongoStarter);
			_mapper = TestUtilsExtension.CreateMapper(new PersistMapperProfile(null	));
			_faqCategoryRepo = new FaqCategoryRepo(_db, _mapper);
		}
		[TearDown]
		public void TearDown()
		{
			_db?.Dispose();
		}
		[Test]
		public void Add_PasValidEntity_CreateEntity()
		{
			var faqCategoryCategory = MockObj("test");
			_faqCategoryRepo.Add(_mapper.Map<FaqCategory>(faqCategoryCategory));
			Assert.AreEqual(_db.FaqCategories.CountDocuments(i => i.Id == faqCategoryCategory.Id), 1);
		}
		[Test]
		public void Add_PasInvalidEntity_ThrowException()
		{
			var faqCategory = new FaqCategory();
			_faqCategoryRepo.Add(faqCategory);
			Assert.AreEqual(_db.FaqCategories.CountDocuments(i => true), 0);
		}
		[Test]
		public void Update_PasValidEntity_UpdateEntity()
		{
			var faqCategory = CreateMockObj();
			faqCategory.Name = "updatedTest";
			_faqCategoryRepo.Update(_mapper.Map<FaqCategory>(faqCategory));

			var updatedFaqCategory = _db.FaqCategories.AsQueryable().FirstOrDefault(i => i.Id == faqCategory.Id);
			Assert.AreEqual(updatedFaqCategory?.Name, faqCategory.Name);
		}
		[Test]
		public void Update_PasInvalidEntity_ThrowException()
		{
			var faqCategory = CreateMockObj();
			var invalidFaqCategory = new FaqCategory()
			{
				Id = faqCategory.Id,
				Name = "updatedTest"
			};
			Assert.Throws<UpdateOperationException>(() =>
			{
				_faqCategoryRepo.Update(invalidFaqCategory);
			});
			Assert.AreNotEqual(invalidFaqCategory.Name, faqCategory.Name);
		}
		[Test]
		public void Delete_PasValidId_DeleteEntity()
		{
			var faqCategory = CreateMockObj();

			_faqCategoryRepo.Delete(faqCategory.Id);

			Assert.IsFalse(_db.FaqCategories.AsQueryable().Any(i => i.Id == faqCategory.Id));
		}
		[Test]
		public void Delete_PasInvalidId_ThrowException()
		{
			var faqCategory = CreateMockObj();
			Assert.Throws<DeleteOperationException>(() =>
			{
				_faqCategoryRepo.Delete(faqCategory.Id);
			});
		}
		[Test]
		public void Get_ReturnAllEntities()
		{
			foreach (var item in Enumerable.Range(1, 3))
			{
				CreateMockObj($"title{item}");
			}

			CollectionAssert.AreEquivalent(_db.FaqCategories.AsQueryable().Select(i => i.Id).ToList(),
				_faqCategoryRepo.Get().Select(i => i.Id));
		}
		[Test]
		public void Get_PassValidQueryParam_ReturnFilteredEntities()
		{
			foreach (var item in Enumerable.Range(1, 10))
			{
				CreateMockObj($"title{item}");
			}
			QueryParams<FaqCategory> queryParams = new QueryParams<FaqCategory>
			{
				Expression = i => true,
				Skip = 1,
				Take = 5,
			};
			CollectionAssert.AreEquivalent(
				_db.FaqCategories.AsQueryable().Skip(1).Take(5).Select(i => i.Id).ToList(),
				_faqCategoryRepo.Get(queryParams).Select(i => i.Id));
		}
		[Test]
		[TestCase()]
		public void Get_PassInvalidQueryParam_ThrowException()
		{
			Assert.Throws<ReadOperationException>(() =>
			{
				_faqCategoryRepo.Get(null);
			});
		}
		#region HelperMethods
		private FaqCategoryDAO MockObj(string question)
		{
			return new FaqCategoryDAO()
			{
				Name = question,
				IsActive = true
			};
		}
		private FaqCategoryDAO CreateMockObj()
		{
			var faqCategory = MockObj("test");
			faqCategory.Id = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 24);
			_db.FaqCategories.InsertOne(faqCategory);
			return faqCategory;
		}
		private FaqCategoryDAO CreateMockObj(string question)
		{
			var faqCategory = MockObj(question);
			faqCategory.Id = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 24);
			_db.FaqCategories.InsertOne(faqCategory);
			return faqCategory;
		}
		#endregion

	}
}
