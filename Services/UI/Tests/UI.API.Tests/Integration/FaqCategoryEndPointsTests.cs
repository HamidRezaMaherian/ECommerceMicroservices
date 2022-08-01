using Microsoft.Extensions.DependencyInjection;
using Mongo2Go;
using MongoDB.Driver;
using NUnit.Framework;
using Services.Shared.APIUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UI.API.Configurations.DTOs;
using UI.API.Tests.Utils;
using UI.Application.Configurations;
using UI.Application.Tools;
using UI.Application.UnitOfWork;
using UI.Domain.Entities;
using UI.Infrastructure.Persist;
using UI.Infrastructure.Persist.Mappings;
namespace UI.API.Tests.Integration
{
	[TestFixture]
	public class FaqCategoryEndPointsTests
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
			_mapper = TestUtilsExtension.CreateMapper(new PersistMapperProfile(null), new ServiceMapper(), new TestMapperProfile());
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
			var faqCategorys = _unitOfWork.FaqCategoryRepo.Get();
			foreach (var item in faqCategorys)
			{
				_unitOfWork.FaqCategoryRepo.Delete(item);
			}
		}
		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			_httpClient.Dispose();
			_unitOfWork.Dispose();
		}
		[Test]
		public void GetAll_ReturnAllFaqCategorys()
		{
			var faqCategory = CreateFaqCategory();
			var res = _httpClient.Get<IEnumerable<FaqCategory>>($"/faqCategory/getall");

			CollectionAssert.AreEquivalent(res.Select(i => i.Id),
				_unitOfWork.FaqCategoryRepo.Get().Select(i => i.Id));
		}
		[Test]
		public void HealthCheck_IsOk()
		{
			var res = _httpClient.Get("/health");
			res.EnsureSuccessStatusCode();
		}
		[Test]
		public void Create_PassValidObject_AddObject()
		{
			var faqCategory = new CreateFaqCategoryDTO()
			{
				Name = Guid.NewGuid().ToString(),
				IsActive = true,
			};
			var res = _httpClient.Post("/faqCategory/create", faqCategory);

			Assert.AreEqual(HttpStatusCode.Created, res.StatusCode);

			Assert.IsTrue(_unitOfWork.FaqCategoryRepo.Exists(i => i.Name == faqCategory.Name));
		}
		[Test]
		public void Create_PassInvalidObject_ReturnBadRequest()
		{
			var faqCategory = new CreateFaqCategoryDTO()
			{
				IsActive = true,
			};
			var res = _httpClient.Post("/faqCategory/create", faqCategory);
			Assert.AreEqual(HttpStatusCode.BadRequest, res.StatusCode);
			Assert.IsFalse(_unitOfWork.FaqCategoryRepo.Exists(i => true));
		}
		[Test]
		public void Update_PassValidObject_UpdateObject()
		{
			var faqCategory = _mapper.Map<UpdateFaqCategoryDTO>(CreateFaqCategory());
			faqCategory.Name = "updated-test";

			var res = _httpClient.Put("/faqCategory/update", faqCategory);
			var updatedFaqCategory = _unitOfWork.FaqCategoryRepo.Get(faqCategory.Id);

			Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
			Assert.AreEqual(updatedFaqCategory.Name, faqCategory.Name);
		}
		[Test]
		public void Update_PassInvalidObject_ReturnBadRequest()
		{
			var faqCategory = _mapper.Map<UpdateFaqCategoryDTO>(CreateFaqCategory());
			faqCategory.Name = "updated-title";
			var res = _httpClient.Put("/faqCategory/update", new
			{
				faqCategory.Id,
			});
			var updatedFaqCategory = _unitOfWork.FaqCategoryRepo.Get(faqCategory.Id);

			Assert.AreEqual(HttpStatusCode.BadRequest, res.StatusCode);
			Assert.AreNotEqual(updatedFaqCategory.Name, faqCategory.Name);
		}
		[Test]
		public void Delete_PassValidId_DeleteRecord()
		{
			var faqCategory = CreateFaqCategory();
			var res = _httpClient.Delete($"/faqCategory/delete/{faqCategory.Id}");
			Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
			Assert.IsNull(_unitOfWork.FaqCategoryRepo.Get(faqCategory.Id));
		}
		[Test]
		public void Delete_PassInvalidId_ReturnNotFound()
		{
			var fakeId = Guid.NewGuid().ToString();
			var res = _httpClient.Delete($"/faqCategory/delete/{fakeId}");
			Assert.AreEqual(HttpStatusCode.NotFound, res.StatusCode);
		}
		#region HelperMethods
		private FaqCategory CreateFaqCategory()
		{
			var faqCategory = new FaqCategory()
			{
				Name = Guid.NewGuid().ToString(),
				IsActive = true
			};
			_unitOfWork.FaqCategoryRepo.Add(faqCategory);
			return faqCategory;
		}

		#endregion
	}
}
