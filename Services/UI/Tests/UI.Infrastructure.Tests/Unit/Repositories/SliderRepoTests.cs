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
	public class SliderRepoTests
	{
		private SliderRepo _sliderRepo;
		private ICustomMapper _mapper;
		private ApplicationDbContext _db;
		private Mongo2Go.MongoDbRunner _mongoStarter;
		[SetUp]
		public void Setup()
		{
			_mongoStarter = Mongo2Go.MongoDbRunner.StartForDebugging();
			_db = MockActions.MockDbContext(_mongoStarter);
			_mapper = TestUtilsExtension.CreateMapper(new PersistMapperProfile(null));
			_sliderRepo = new SliderRepo(_db, _mapper);
		}
		[TearDown]
		public void TearDown()
		{
			_db?.Dispose();
		}
		[Test]
		public void Add_PasValidEntity_CreateEntity()
		{
			var slider = MockObj("test");
			_sliderRepo.Add(_mapper.Map<Slider>(slider));
			Assert.AreEqual(_db.Sliders.CountDocuments(i => i.Id == slider.Id), 1);
		}
		[Test]
		public void Add_PasInvalidEntity_ThrowException()
		{
			var slider = new Slider();
			_sliderRepo.Add(slider);
			Assert.AreEqual(_db.Sliders.CountDocuments(i => true), 0);
		}
		[Test]
		public void Update_PasValidEntity_UpdateEntity()
		{
			var slider = CreateMockObj();
			slider.Title = "updatedTest";
			_sliderRepo.Update(_mapper.Map<Slider>(slider));

			var updatedSlider = _db.Sliders.AsQueryable().FirstOrDefault(i => i.Id == slider.Id);
			Assert.AreEqual(updatedSlider?.Title, slider.Title);
		}
		[Test]
		public void Update_PasInvalidEntity_ThrowException()
		{
			var slider = CreateMockObj();
			var invalidSlider = new Slider()
			{
				Id = slider.Id,
				Title = "updatedTest"
			};
			Assert.Throws<UpdateOperationException>(() =>
			{
				_sliderRepo.Update(invalidSlider);
			});
			Assert.AreNotEqual(invalidSlider.Title, slider.Title);
		}
		[Test]
		public void Delete_PasValidId_DeleteEntity()
		{
			var slider = CreateMockObj();

			_sliderRepo.Delete(slider.Id);

			Assert.IsFalse(_db.Sliders.AsQueryable().Any(i => i.Id == slider.Id));
		}
		[Test]
		public void Delete_PasInvalidId_ThrowException()
		{
			var slider = CreateMockObj();
			Assert.Throws<DeleteOperationException>(() =>
			{
				_sliderRepo.Delete(slider.Id);
			});
		}
		[Test]
		public void Get_ReturnAllEntities()
		{
			foreach (var item in Enumerable.Range(1, 3))
			{
				CreateMockObj($"title{item}");
			}

			CollectionAssert.AreEquivalent(_db.Sliders.AsQueryable().Select(i => i.Id).ToList(),
				_sliderRepo.Get().Select(i => i.Id));
		}
		[Test]
		public void Get_PassValidQueryParam_ReturnFilteredEntities()
		{
			foreach (var item in Enumerable.Range(1, 10))
			{
				CreateMockObj($"title{item}");
			}
			QueryParams<Slider> queryParams = new QueryParams<Slider>
			{
				Expression = i => true,
				Skip = 1,
				Take = 5,
			};
			CollectionAssert.AreEquivalent(
				_db.Sliders.AsQueryable().Skip(1).Take(5).Select(i => i.Id).ToList(),
				_sliderRepo.Get(queryParams).Select(i => i.Id));
		}
		[Test]
		public void Get_PassInvalidQueryParam_ThrowException()
		{
			Assert.Throws<ReadOperationException>(() =>
			{
				_sliderRepo.Get(null);
			});
		}
		#region HelperMethods
		private SliderDAO MockObj(string title)
		{
			return new SliderDAO()
			{
				Title = title,
				ImagePath = "no image",
				IsActive = true
			};
		}
		private SliderDAO CreateMockObj()
		{
			var slider = MockObj("title");
			slider.Id = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 24);
			_db.Sliders.InsertOne(slider);
			return slider;
		}
		private SliderDAO CreateMockObj(string title)
		{
			var slider = MockObj(title);
			slider.Id = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 24);
			_db.Sliders.InsertOne(slider);
			return slider;
		}
		#endregion

	}
}
