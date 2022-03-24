using Microsoft.Extensions.DependencyInjection;
using Mongo2Go;
using MongoDB.Driver;
using NUnit.Framework;
using Services.Shared.APIUtils;
using Services.Shared.Contracts;
using Services.Shared.Tests;
using System.Linq;
using System.Net;
using UI.API.Tests.Utils;
using UI.Application.Configurations;
using UI.Application.DTOs;
using UI.Application.UnitOfWork;
using UI.Domain.Entities;
using UI.Infrastructure.Persist;
using UI.Infrastructure.Persist.Mappings;
namespace UI.API.Tests.Integration
{
	[TestFixture]
	public class InfoEndPointsTests
	{
		private HttpRequestHelper _httpClient;
		private IUnitOfWork _unitOfWork;
		private MongoDbRunner _mongoDbRunner;
		private ICustomMapper _mapper;
		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_mapper = TestUtilsExtension.CreateMapper(new PersistMapperProfile(), new ServiceMapper());
			_mongoDbRunner = MongoDbRunner.Start();
			var db = MockActions.MockDbContext(_mongoDbRunner);
			_unitOfWork = MockActions.MockUnitOfWork
			(db, _mapper);

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
			CreateAboutUs();
			CreateContactUs();
		}

		[TearDown]
		public void TearDown()
		{
		}
		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			_httpClient.Dispose();
			_unitOfWork.Dispose();
		}
		[Test]
		public void GetAboutUs_ReturnAboutUs()
		{
			var res = _httpClient.Get<AboutUs>($"/info/aboutUs");

			Assert.AreEqual(_unitOfWork.AboutUsRepo.Get().FirstOrDefault()?.Id, res.Id);
		}
		[Test]
		public void GetContactUs_ReturnContactUs()
		{
			var res = _httpClient.Get<ContactUs>($"/info/contactUs");

			Assert.AreEqual(_unitOfWork.ContactUsRepo.Get().FirstOrDefault()?.Id, res.Id);
		}
		[Test]
		public void UpdateContactUs_PassValidObject_UpdateObject()
		{
			var contactUs = _mapper.Map<ContactUsDTO>(
				_unitOfWork.ContactUsRepo.Get().FirstOrDefault()
				);
			contactUs.Email = "updatedtest@test.com";
			var res = _httpClient.Put("/info/contactUs", contactUs);
			var updatedContactUs = _unitOfWork.ContactUsRepo.Get().FirstOrDefault();

			Assert.AreEqual(HttpStatusCode.OK,res.StatusCode);
			Assert.AreEqual(contactUs.Email, updatedContactUs?.Email);
		}
		[Test]
		public void UpdateContactUs_PassInvalidObject_ReturnBadRequest()
		{
			var contactUs = _unitOfWork.ContactUsRepo.Get().FirstOrDefault();
			contactUs.Email = "updatedtest@test.com";
			var res = _httpClient.Put("/info/contactUs", new
			{
				Email = contactUs.Email
			});
			var updatedContactUs = _unitOfWork.ContactUsRepo.Get().FirstOrDefault();

			Assert.AreEqual(HttpStatusCode.BadRequest,res.StatusCode);
			Assert.AreNotEqual(updatedContactUs?.Email, contactUs.Email);
		}
		[Test]
		public void UpdateAboutUs_PassValidObject_UpdateObject()
		{
			var aboutUs = _mapper.Map<AboutUsDTO>(
				_unitOfWork.AboutUsRepo.Get().FirstOrDefault());
			aboutUs.Title = "updatedtest";
			var res = _httpClient.Put("/info/aboutUs/", aboutUs);
			var updatedAboutUs = _unitOfWork.AboutUsRepo.Get().FirstOrDefault();

			Assert.AreEqual(HttpStatusCode.OK,res.StatusCode);
			Assert.AreEqual(aboutUs.Title, updatedAboutUs?.Title);
		}
		[Test]
		public void UpdateAboutUs_PassInvalidObject_ReturnBadRequest()
		{
			var aboutUs = _unitOfWork.AboutUsRepo.Get().FirstOrDefault();
			aboutUs.Title = "updatedtest";
			var res = _httpClient.Put("/info/aboutUs", new
			{
				Title = aboutUs.Title
			});
			var updatedAboutUs = _unitOfWork.AboutUsRepo.Get().FirstOrDefault();

			Assert.AreEqual(HttpStatusCode.BadRequest, res.StatusCode);
			Assert.AreNotEqual(updatedAboutUs?.Title, aboutUs.Title);
		}
		#region HelperMethods
		private AboutUs CreateAboutUs()
		{
			var aboutUs = new AboutUs()
			{
				Title = "test title",
				Description = "no desc",
				ShortDesc = "no short desc",
				ImagePath = "no image"
			};
			_unitOfWork.AboutUsRepo.Add(aboutUs);
			return aboutUs;
		}
		private ContactUs CreateContactUs()
		{
			var contactUs = new ContactUs()
			{
				Email = "test@test.com",
				Address = "no address",
				Location = "no location",
				Lat = "192391",
				Lng = "234234",
				PhoneNumber = "09304422204"
			};
			_unitOfWork.ContactUsRepo.Add(contactUs);
			return  contactUs;
		}

		#endregion
	}
}
