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
	public class SocialMediaEndPointsTests
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
			var socialMedias = _unitOfWork.SocialMediaRepo.Get();
			foreach (var item in socialMedias)
			{
				_unitOfWork.SocialMediaRepo.Delete(item);
			}
		}
		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			_httpClient.Dispose();
			_unitOfWork.Dispose();
		}
		[Test]
		public void GetAll_ReturnAllSocialMedias()
		{
			var socialMedia = CreateSocialMedia();
			var res = _httpClient.Get<IEnumerable<SocialMedia>>($"/socialMedia/getall");

			CollectionAssert.AreEquivalent(res.Select(i => i.Id),
				_unitOfWork.SocialMediaRepo.Get().Select(i => i.Id));
		}
		[Test]
		public void Create_PassValidObject_AddObject()
		{
			var socialMedia = new SocialMediaDTO()
			{
				Name = Guid.NewGuid().ToString(),
				Link = "no link",
				ImagePath = "no image",
				IsActive = true,
			};
			var res = _httpClient.Post("/socialMedia/create", socialMedia);

			Assert.AreEqual(HttpStatusCode.Created, res.StatusCode);

			Assert.IsTrue(_unitOfWork.SocialMediaRepo.Exists(i => i.Name == socialMedia.Name));
		}
		[Test]
		public void Create_PassInvalidObject_ReturnBadRequest()
		{
			var socialMedia = new SocialMediaDTO()
			{
				Name = Guid.NewGuid().ToString(),
			};
			var res = _httpClient.Post("/socialMedia/create", socialMedia);
			Assert.AreEqual(HttpStatusCode.BadRequest, res.StatusCode);
			Assert.IsFalse(_unitOfWork.SocialMediaRepo.Exists(i => i.Name == socialMedia.Name));
		}
		[Test]
		public void Update_PassValidObject_UpdateObject()
		{
			var socialMedia = CreateSocialMedia();
			socialMedia.Name = "updated-test";
			var res = _httpClient.Put("/socialMedia/update", socialMedia);
			var updatedSocialMedia = _mapper.Map<SocialMediaDTO>(
				_unitOfWork.SocialMediaRepo.Get(socialMedia.Id));

			Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
			Assert.AreEqual(updatedSocialMedia.Name, socialMedia.Name);
		}
		[Test]
		public void Update_PassInvalidObject_ReturnBadRequest()
		{
			var socialMedia = CreateSocialMedia();
			socialMedia.Name = "updated-title";
			var res = _httpClient.Put("/socialMedia/update", new
			{
				socialMedia.Id,
			});
			var updatedSocialMedia = _mapper.Map<SocialMediaDTO>(
				_unitOfWork.SocialMediaRepo.Get(socialMedia.Id));

			Assert.AreEqual(HttpStatusCode.BadRequest, res.StatusCode);
			Assert.AreNotEqual(updatedSocialMedia.Name, socialMedia.Name);
		}
		[Test]
		public void Delete_PassValidId_DeleteRecord()
		{
			var socialMedia = CreateSocialMedia();
			var res = _httpClient.Delete($"/socialMedia/delete/{socialMedia.Id}");
			Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
			Assert.IsNull(_unitOfWork.SocialMediaRepo.Get(socialMedia.Id));
		}
		[Test]
		public void Delete_PassInvalidId_ReturnNotFound()
		{
			var fakeId = Guid.NewGuid().ToString();
			var res = _httpClient.Delete($"/socialMedia/delete/{fakeId}");
			Assert.AreEqual(HttpStatusCode.NotFound, res.StatusCode);
		}
		#region HelperMethods
		private SocialMedia CreateSocialMedia()
		{
			var socialMedia = new SocialMedia()
			{
				Name = "no name",
				Link = "no link",
				ImagePath = "no image",
				IsActive = true
			};
			_unitOfWork.SocialMediaRepo.Add(socialMedia);
			return socialMedia;
		}

		#endregion
	}
}
