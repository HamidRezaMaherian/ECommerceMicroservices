﻿using Microsoft.Extensions.DependencyInjection;
using Mongo2Go;
using MongoDB.Driver;
using NUnit.Framework;
using Services.Shared.APIUtils;
using Services.Shared.Contracts;
using Services.Shared.Tests;
using System;
using System.Collections.Generic;
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
	public class SliderEndPointsTests
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
			var slider = CreateSlider();
			var res = _httpClient.Get<IEnumerable<Slider>>($"/slider/getall");

			CollectionAssert.AreEquivalent(res.Select(i => i.Id),
				_unitOfWork.SliderRepo.Get().Select(i => i.Id));
		}
		[Test]
		public void Create_PassValidObject_AddObject()
		{
			var slider = new SliderDTO()
			{
				Title = Guid.NewGuid().ToString(),
				ImagePath = "no image",
				IsActive = true,
			};
			var res = _httpClient.Post("/slider/create", slider);

			Assert.AreEqual(HttpStatusCode.OK,res.StatusCode);

			Assert.IsTrue(_unitOfWork.SliderRepo.Exists(i => i.Title == slider.Title));
		}
		[Test]
		public void Create_PassInvalidObject_ReturnBadRequest()
		{
			var slider = new SliderDTO();
			var res = _httpClient.Post("/slider/create", slider);
			Assert.AreEqual(HttpStatusCode.BadRequest,res.StatusCode);
			Assert.IsFalse(_unitOfWork.SliderRepo.Exists(i => i.Title == slider.Title));
		}
		[Test]
		public void Update_PassValidObject_UpdateObject()
		{
			var slider = CreateSlider();
			slider.Title = "updated-test";
			var res = _httpClient.Put("/slider/update", slider);
			var updatedSlider = _mapper.Map<SliderDTO>(
				_unitOfWork.SliderRepo.Get(slider.Id));

			Assert.AreEqual(HttpStatusCode.OK,res.StatusCode);
			Assert.AreEqual(updatedSlider.Title, slider.Title);
		}
		[Test]
		public void Update_PassInvalidObject_ReturnBadRequest()
		{
			var slider = CreateSlider();
			slider.Title = "updated-title";
			var res = _httpClient.Put("/slider/update", new
			{
				Id = slider.Id,
			});
			var updatedSlider = _mapper.Map<SliderDTO>(
				_unitOfWork.SliderRepo.Get(slider.Id));

			Assert.AreEqual(HttpStatusCode.BadRequest,res.StatusCode);
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
		private SliderDTO CreateSlider()
		{
			var slider = new Slider()
			{
				ImagePath = "no image",
				Title = "no title",
				IsActive = true
			};
			_unitOfWork.SliderRepo.Add(slider);
			return _mapper.Map<SliderDTO>(slider);
		}
		#endregion
	}
}