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
	public class FaqRepoTests
	{
		private FaqRepo _faqRepo;
		private ICustomMapper _mapper;
		private ApplicationDbContext _db;
		private Mongo2Go.MongoDbRunner _mongoStarter;
		[SetUp]
		public void Setup()
		{
			_mongoStarter = Mongo2Go.MongoDbRunner.StartForDebugging();
			_db = MockActions.MockDbContext(_mongoStarter);
			_mapper = TestUtilsExtension.CreateMapper(new PersistMapperProfile(null));
			_faqRepo = new FaqRepo(_db, _mapper);
		}
		[TearDown]
		public void TearDown()
		{
			_db?.Dispose();
		}
		[Test]
		public void Add_PasValidEntity_CreateEntity()
		{
			var faq = CreateMockObj();
			Assert.AreEqual(_db.Faqs.CountDocuments(i => i.Id == faq.Id), 1);
		}
		[Test]
		public void Add_PasInvalidEntity_ThrowException()
		{
			var faq = new FAQ();
			_faqRepo.Add(faq);
			Assert.AreEqual(_db.Faqs.CountDocuments(i => true), 0);
		}
		[Test]
		public void Update_PasValidEntity_UpdateEntity()
		{
			var faq = CreateMockObj();
			faq.Question = "updatedTest";
			_faqRepo.Update(_mapper.Map<FAQ>(faq));

			var updatedFaq = _db.Faqs.AsQueryable().FirstOrDefault(i => i.Id == faq.Id);
			Assert.AreEqual(updatedFaq?.Question, faq.Question);
		}
		[Test]
		public void Update_PasInvalidEntity_ThrowException()
		{
			var faq = CreateMockObj();
			var invalidFaq = new FAQ()
			{
				Id = faq.Id,
				Question = "updatedTest"
			};
			Assert.Throws<UpdateOperationException>(() =>
			{
				_faqRepo.Update(invalidFaq);
			});
			Assert.AreNotEqual(invalidFaq.Question, faq.Question);
		}
		[Test]
		public void Delete_PasValidId_DeleteEntity()
		{
			var faq = CreateMockObj();

			_faqRepo.Delete(faq.Id);

			Assert.IsFalse(_db.Faqs.AsQueryable().Any(i => i.Id == faq.Id));
		}
		[Test]
		public void Delete_PasInvalidId_ThrowException()
		{
			var faq = CreateMockObj();
			Assert.Throws<DeleteOperationException>(() =>
			{
				_faqRepo.Delete(faq.Id);
			});
		}
		[Test]
		public void Get_ReturnAllEntities()
		{
			foreach (var item in Enumerable.Range(1, 3))
			{
				CreateMockObj($"title{item}");
			}

			CollectionAssert.AreEquivalent(_db.Faqs.AsQueryable().Select(i => i.Id).ToList(),
				_faqRepo.Get().Select(i => i.Id));
		}
		[Test]
		public void Get_PassValidQueryParam_ReturnFilteredEntities()
		{
			foreach (var item in Enumerable.Range(1, 10))
			{
				CreateMockObj($"title{item}");
			}
			QueryParams<FAQ> queryParams = new QueryParams<FAQ>
			{
				Expression = i => true,
				Skip = 1,
				Take = 5,
			};
			CollectionAssert.AreEquivalent(
				_db.Faqs.AsQueryable().Skip(1).Take(5).Select(i => i.Id).ToList(),
				_faqRepo.Get(queryParams).Select(i => i.Id));
		}
		[Test]
		[TestCase()]
		public void Get_PassInvalidQueryParam_ThrowException()
		{
			Assert.Throws<ReadOperationException>(() =>
			{
				_faqRepo.Get(null);
			});
		}
		#region HelperMethods
		private FaqDAO MockObj(string question)
		{
			return new FaqDAO()
			{
				Question = question,
				Answer = "no a",
				IsActive = true
			};
		}
		private FaqCategoryDAO CreateMockObjCategory()
		{
			var faqCategory = new FaqCategoryDAO()
			{
				Name = "test",
				IsActive = true
			};
			faqCategory.Id = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 24);
			_db.FaqCategories.InsertOne(faqCategory);
			return faqCategory;

		}
		private FaqDAO CreateMockObj()
		{
			var faqCategory = CreateMockObjCategory();
			var faq = MockObj("test");
			faq.CategoryId = faqCategory.Id;
			faq.Id = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 24);
			_db.Faqs.InsertOne(faq);
			return faq;
		}
		private FaqDAO CreateMockObj(string question)
		{
			var faq = MockObj(question);
			faq.Id = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 24);
			_db.Faqs.InsertOne(faq);
			return faq;
		}
		#endregion

	}
}
