using Microsoft.Extensions.DependencyInjection;
using Mongo2Go;
using MongoDB.Driver;
using NUnit.Framework;
using Services.Shared.APIUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UI.API.Tests.Utils;
using UI.Application.Configurations;
using UI.Application.DTOs;
using UI.Application.Tools;
using UI.Application.UnitOfWork;
using UI.Domain.Entities;
using UI.Infrastructure.Persist;
using UI.Infrastructure.Persist.Mappings;
namespace UI.API.Tests.Integration
{
	[TestFixture]
	public class FaqEndPointsTests
	{
		private HttpRequestHelper _httpClient;
		private IUnitOfWork _unitOfWork;
		private ICustomMapper _mapper;
		private MongoDbRunner _mongoDbRunner;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_mongoDbRunner = MongoDbRunner.Start();
			var db = MockActions.MockDbContext(_mongoDbRunner);
			_mapper = TestUtilsExtension.CreateMapper(new PersistMapperProfile(), new ServiceMapper());
			_unitOfWork = MockActions.MockUnitOfWork(db, _mapper);

			var httpClient = new TestingWebAppFactory<Program>(s =>
			{
				var dbContextConfiguration = s.SingleOrDefault(opt => opt.ServiceType == typeof(ApplicationDbContext));
				if (dbContextConfiguration != null)
					s.Remove(dbContextConfiguration);
				s.AddScoped(opt =>
				{
					return MockActions.MockDbContext(_mongoDbRunner);
				});
			}).CreateClient();
			_httpClient = new HttpRequestHelper(httpClient);
		}
		[TearDown]
		public void TearDown()
		{
			var faqs = _unitOfWork.FaqRepo.Get();
			foreach (var item in faqs)
			{
				_unitOfWork.FaqRepo.Delete(item);
			}
		}
		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			_httpClient.Dispose();
			_unitOfWork.Dispose();
		}
		[Test]
		public void GetAll_ReturnAllFaqs()
		{
			var faq = CreateFaq();
			var res = _httpClient.Get<IEnumerable<FAQ>>($"/faq/getall");

			CollectionAssert.AreEquivalent(res.Select(i => i.Id),
				_unitOfWork.FaqRepo.Get().Select(i => i.Id));
		}
		[Test]
		public void Create_PassValidObject_AddObject()
		{
			var faq = new FaqDTO()
			{
				Question = "no q",
				Answer = "no a",
				CategoryId = CreateCategory().Id,
				IsActive = true,
			};
			var res = _httpClient.Post("/faq/create", faq);

			Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);

			Assert.IsTrue(_unitOfWork.FaqRepo.Exists(i => i.Question == faq.Question));
		}

		[Test]
		public void Create_PassInvalidObject_ReturnBadRequest()
		{
			var faq = new FaqDTO()
			{
				Question = "no q",
				IsActive = true,
			};
			var res = _httpClient.Post("/faq/create", faq);
			Assert.AreEqual(HttpStatusCode.BadRequest, res.StatusCode);
			Assert.IsFalse(_unitOfWork.FaqRepo.Exists(i => i.Question == faq.Question));
		}
		[Test]
		public void Update_PassValidObject_UpdateObject()
		{
			var faq = CreateFaq();
			faq.Question = "updated-test";
			var res = _httpClient.Put("/faq/update", faq);
			var updatedFaq = _mapper.Map<FaqDTO>(_unitOfWork.FaqRepo.Get(faq.Id));

			Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
			Assert.AreEqual(updatedFaq.Question, faq.Question);
		}
		[Test]
		public void Update_PassInvalidObject_ReturnBadRequest()
		{
			var faq = CreateFaq();
			faq.Question = "updated-title";
			var res = _httpClient.Put("/faq/update", new
			{
				faq.Id,
			});
			var updatedFaq = _mapper.Map<FaqDTO>(_unitOfWork.FaqRepo.Get(faq.Id));

			Assert.AreEqual(HttpStatusCode.BadRequest, res.StatusCode);
			Assert.AreNotEqual(updatedFaq.Question, faq.Question);
		}
		[Test]
		public void Delete_PassValidId_DeleteRecord()
		{
			var faq = CreateFaq();
			var res = _httpClient.Delete($"/faq/delete/{faq.Id}");
			Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
			Assert.IsNull(_unitOfWork.FaqRepo.Get(faq.Id));
		}
		[Test]
		public void Delete_PassInvalidId_ReturnNotFound()
		{
			var fakeId = Guid.NewGuid().ToString();
			var res = _httpClient.Delete($"/faq/delete/{fakeId}");
			Assert.AreEqual(HttpStatusCode.NotFound, res.StatusCode);
		}
		#region HelperMethods
		private FaqDTO CreateFaq()
		{
			var faq = new FAQ()
			{
				IsActive = true,
				CategoryId = CreateCategory().Id,
				Question = "no q",
				Answer = "no a"
			};
			_unitOfWork.FaqRepo.Add(faq);
			return _mapper.Map<FaqDTO>(faq);
		}
		private FaqCategory CreateCategory()
		{
			var faqCategory = new FaqCategory()
			{
				Name = "testC",
				IsActive = true,
			};
			_unitOfWork.FaqCategoryRepo.Add(faqCategory);
			return faqCategory;
		}
		#endregion
	}
}
