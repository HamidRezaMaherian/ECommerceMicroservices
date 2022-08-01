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
	public class SocialMediaRepoTests
	{
		private SocialMediaRepo _socialMediaRepo;
		private ICustomMapper _mapper;
		private ApplicationDbContext _db;
		private Mongo2Go.MongoDbRunner _mongoStarter;
		[SetUp]
		public void Setup()
		{
			_mongoStarter = Mongo2Go.MongoDbRunner.StartForDebugging();
			_db = MockActions.MockDbContext(_mongoStarter);
			_mapper = TestUtilsExtension.CreateMapper(new PersistMapperProfile(null));
			_socialMediaRepo = new SocialMediaRepo(_db, _mapper);
		}
		[TearDown]
		public void TearDown()
		{
			_db?.Dispose();
		}
		[Test]
		public void Add_PasValidEntity_CreateEntity()
		{
			var socialMedia = MockObj("test");
			_socialMediaRepo.Add(_mapper.Map<SocialMedia>(socialMedia));
			Assert.AreEqual(_db.SocialMedias.CountDocuments(i => i.Id == socialMedia.Id), 1);
		}
		[Test]
		public void Add_PasInvalidEntity_ThrowException()
		{
			var socialMedia = new SocialMedia();
			_socialMediaRepo.Add(socialMedia);
			Assert.AreEqual(_db.SocialMedias.CountDocuments(i => true), 0);
		}
		[Test]
		public void Update_PasValidEntity_UpdateEntity()
		{
			var socialMedia = CreateMockObj();
			socialMedia.Name = "updatedTest";
			_socialMediaRepo.Update(_mapper.Map<SocialMedia>(socialMedia));

			var updatedSocialMedia = _db.SocialMedias.AsQueryable().FirstOrDefault(i => i.Id == socialMedia.Id);
			Assert.AreEqual(updatedSocialMedia?.Name, socialMedia.Name);
		}
		[Test]
		public void Update_PasInvalidEntity_ThrowException()
		{
			var socialMedia = CreateMockObj();
			var invalidSocialMedia = new SocialMedia()
			{
				Id = socialMedia.Id,
				Name = "updatedTest"
			};
			Assert.Throws<UpdateOperationException>(() =>
			{
				_socialMediaRepo.Update(invalidSocialMedia);
			});
			Assert.AreNotEqual(invalidSocialMedia.Name, socialMedia.Name);
		}
		[Test]
		public void Delete_PasValidId_DeleteEntity()
		{
			var socialMedia = CreateMockObj();

			_socialMediaRepo.Delete(socialMedia.Id);

			Assert.IsFalse(_db.SocialMedias.AsQueryable().Any(i => i.Id == socialMedia.Id));
		}
		[Test]
		public void Delete_PasInvalidId_ThrowException()
		{
			var socialMedia = CreateMockObj();
			Assert.Throws<DeleteOperationException>(() =>
			{
				_socialMediaRepo.Delete(socialMedia.Id);
			});
		}
		[Test]
		public void Get_ReturnAllEntities()
		{
			foreach (var item in Enumerable.Range(1, 3))
			{
				CreateMockObj($"title{item}");
			}

			CollectionAssert.AreEquivalent(_db.SocialMedias.AsQueryable().Select(i => i.Id).ToList(),
				_socialMediaRepo.Get().Select(i => i.Id));
		}
		[Test]
		public void Get_PassValidQueryParam_ReturnFilteredEntities()
		{
			foreach (var item in Enumerable.Range(1, 10))
			{
				CreateMockObj($"title{item}");
			}
			QueryParams<SocialMedia> queryParams = new QueryParams<SocialMedia>
			{
				Expression = i => true,
				Skip = 1,
				Take = 5,
			};
			CollectionAssert.AreEquivalent(
				_db.SocialMedias.AsQueryable().Skip(1).Take(5).Select(i => i.Id).ToList(),
				_socialMediaRepo.Get(queryParams).Select(i => i.Id));
		}
		[Test]
		public void Get_PassInvalidQueryParam_ThrowException()
		{
			Assert.Throws<ReadOperationException>(() =>
			{
				_socialMediaRepo.Get(null);
			});
		}
		#region HelperMethods
		private SocialMediaDAO MockObj(string name)
		{
			return new SocialMediaDAO()
			{
				Name = name,
				ImagePath = "no image",
				IsActive = true
			};
		}
		private SocialMediaDAO CreateMockObj()
		{
			var socialMedia = MockObj("name");
			socialMedia.Id = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 24);
			_db.SocialMedias.InsertOne(socialMedia);
			return socialMedia;
		}
		private SocialMediaDAO CreateMockObj(string title)
		{
			var socialMedia = MockObj(title);
			socialMedia.Id = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 24);
			_db.SocialMedias.InsertOne(socialMedia);
			return socialMedia;
		}
		#endregion

	}
}
