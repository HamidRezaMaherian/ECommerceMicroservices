using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using NUnit.Framework;
using Services.Shared.APIUtils;
using Services.Shared.AppUtils;
using Services.Shared.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UI.API.Tests.Utils;
using UI.Application.DTOs;
using UI.Application.UnitOfWork;
using UI.Domain.Entities;
using UI.Infrastructure.Persist;
using UI.Infrastructure.Persist.Mappings;

namespace UI.API.Tests.Integration
{
	[TestFixture]
	public class SliderEndPointsTests
	{
		private HttpRequestHelper _httpClient;
		private IUnitOfWork _unitOfWork;
		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			var testDB = new Dictionary<string, IList<object>>();
			string dbName = "test_db";
			var dbContext = MockActions.MockDbContext(dbName, testDB);

			_unitOfWork = new UnitOfWork(dbContext,
				TestUtilsExtension.CreateMapper(new PersistMapperProfile()));

			var httpClient = new TestingWebAppFactory<Program>(s =>
			{
				var dbContextConfiguration = s.SingleOrDefault(opt => opt.ServiceType == typeof(MongoClient));
				var applicationDbContext = s.SingleOrDefault(opt => opt.ServiceType == typeof(ApplicationDbContext));
				if (dbContextConfiguration != null)
					s.Remove(dbContextConfiguration);
				s.AddScoped(opt =>
				{
					return dbContext;
				});
			}).CreateClient();
			_httpClient = new HttpRequestHelper(httpClient);
		}
		[TearDown]
		public void TearDown()
		{
			var sliders = _unitOfWork.SliderRepo.Get();
			foreach (var item in sliders)
			{
				_unitOfWork.SliderRepo.Delete(item);
			}
		}
		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			_httpClient.Dispose();
			_unitOfWork.Dispose();
		}
		[Test]
		public void GetAll_ReturnAllSliders()
		{
			var res = _httpClient.Get("/slider/getall");

			var resList = JsonHelper.Parse<IEnumerable<Slider>>(res.Content.ReadAsStringAsync().Result);

			CollectionAssert.AreEquivalent(resList.Select(i => i.Id),
				_unitOfWork.SliderRepo.Get().Select(i => i.Id));
		}
		[Test]
		public void Create_PassValidObject_AddObject()
		{
			var slider = new SliderDTO()
			{
				Title = "no title",
				ImagePath = "no image",
				IsActive = true
			};
			var res = _httpClient.Post("/slider/create", slider);

			Assert.AreEqual(res.StatusCode, HttpStatusCode.OK);

			Assert.IsTrue(_unitOfWork.SliderRepo.Exists(i => i.Title == slider.Title));
		}
		[Test]
		public void Create_PassInvalidObject_ReturnBadRequest()
		{
			var slider = new SliderDTO();

			var res = _httpClient.Post("/slider/create", slider);

			Assert.AreEqual(res.StatusCode, HttpStatusCode.BadRequest);
			Assert.IsFalse(_unitOfWork.SliderRepo.Exists(i => i.Title == slider.Title));
		}
		[Test]
		public void Update_PassValidObject_UpdateObject()
		{
			var slider = CreateSlider();
			slider.Title = "updatedTest";
			var res = _httpClient.Put("/slider/update", slider);
			var updatedSlider = _unitOfWork.SliderRepo.Get(slider.Id);

			Assert.AreEqual(res.StatusCode, HttpStatusCode.OK);
			Assert.AreEqual(updatedSlider.Title, slider.Title);
		}
		[Test]
		public void Update_PassInvalidObject_ReturnBadRequest()
		{
			var slider = CreateSlider();
			slider.Title = "test2";
			var res = _httpClient.Put("/slider/update", new
			{
				Id = slider.Id,
			});
			var updatedSlider = _unitOfWork.SliderRepo.Get(slider.Id);

			Assert.AreEqual(res.StatusCode, HttpStatusCode.BadRequest);
			Assert.AreNotEqual(updatedSlider.Title, slider.Title);
		}
		[Test]
		public void Delete_PassValidId_DeleteRecord()
		{
			var slider = CreateSlider();
			var res = _httpClient.Delete($"/slider/delete/{slider.Id}");
			Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
			Assert.IsNull(_unitOfWork.SliderRepo.Get(slider.Id));
		}
		[Test]
		public void Delete_PassInvalidId_ReturnNotFound()
		{
			var fakeId = Guid.NewGuid().ToString();
			var res = _httpClient.Delete($"/slider/delete/{fakeId}");
			Assert.AreEqual(HttpStatusCode.NotFound, res.StatusCode);
		}
		#region HelperMethods
		private Slider CreateSlider()
		{
			var store = new Slider()
			{
				Title = "no title",
				ImagePath = "no image",
				IsActive = true
			};
			_unitOfWork.SliderRepo.Add(store);
			return store;
		}

		#endregion
	}
}
