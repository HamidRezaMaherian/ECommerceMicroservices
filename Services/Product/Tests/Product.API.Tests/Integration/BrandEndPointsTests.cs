using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Product.API.Tests.Utils;
using Product.Application.Configurations;
using Product.Application.DTOs;
using Product.Application.Tools;
using Product.Application.UnitOfWork;
using Product.Domain.Entities;
using Product.Infrastructure.Persist;
using Product.Infrastructure.Persist.Mappings;
using Services.Shared.APIUtils;
using Services.Shared.AppUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
namespace Product.API.Tests.Integration
{
	[TestFixture]
	public class BrandEndPointsTests
	{
		private HttpRequestHelper _httpClient;
		private IUnitOfWork _unitOfWork;
		private ICustomMapper _mapper;
		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			string dbName = "test_db";
			var dbContext = MockActions.MockDbContext(dbName);
			_mapper = TestUtilsExtension.CreateMapper(new PersistMapperProfile(), new ServiceMapper());
			_unitOfWork = new UnitOfWork(dbContext, _mapper);

			var httpClient = new TestingWebAppFactory<Program>(s =>
			{
				var dbContextConfiguration = s.SingleOrDefault(opt => opt.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
				if (dbContextConfiguration != null)
					s.Remove(dbContextConfiguration);
				s.AddDbContextPool<ApplicationDbContext>(opt => opt.UseInMemoryDatabase(dbName));
			}).CreateClient();
			_httpClient = new HttpRequestHelper(httpClient);
		}
		[TearDown]
		public void TearDown()
		{
			var brands = _unitOfWork.BrandRepo.Get();
			foreach (var item in brands)
			{
				_unitOfWork.BrandRepo.Delete(item);
			}
			_unitOfWork.Save();
		}
		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			_httpClient.Dispose();
			_unitOfWork.Dispose();
		}
		[Test]
		public void GetAll_ReturnAllBrands()
		{
			var res = _httpClient.Get("/brand/getall");

			var resList = JsonHelper.Parse<IEnumerable<Brand>>(res.Content.ReadAsStringAsync().Result);

			CollectionAssert.AreEquivalent(resList.Select(i => i.Id),
				_unitOfWork.BrandRepo.Get().Select(i => i.Id));
		}
		[Test]
		public void Create_PassValidObject_AddObject()
		{
			var brand = new BrandDTO()
			{
				Name = "test",
				ImagePath = "no image"
			};
			var res = _httpClient.Post("/brand/create", brand);

			Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);

			Assert.IsTrue(_unitOfWork.BrandRepo.Exists(i => i.Name == brand.Name));
		}
		[Test]
		public void Create_PassInvalidObject_ReturnBadRequest()
		{
			var brand = new BrandDTO();
			var res = _httpClient.Post("/brand/create", brand);

			Assert.AreEqual(HttpStatusCode.BadRequest, res.StatusCode);
			Assert.IsFalse(_unitOfWork.BrandRepo.Exists(i => i.Name == "test"));
		}
		[Test]
		public void Update_PassValidObject_UpdateObject()
		{
			var brand = CreateBrand();
			brand.Name = "test2";
			var res = _httpClient.Put("/brand/update", brand);
			var updatedBrand = _mapper.Map<BrandDTO>(_unitOfWork.BrandRepo.Get(brand.Id));

			Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
			Assert.AreEqual(updatedBrand.Name, brand.Name);
		}
		[Test]
		public void Update_PassInvalidObject_ReturnBadRequest()
		{
			var brand = CreateBrand();
			brand.Name = "test2";
			var res = _httpClient.Put("/brand/update", new
			{
				Id = brand.Id,
			});
			var updatedBrand = _mapper.Map<BrandDTO>(_unitOfWork.BrandRepo.Get(brand.Id));

			Assert.AreEqual(HttpStatusCode.BadRequest, res.StatusCode);
			Assert.AreNotEqual(updatedBrand.Name, brand.Name);
		}
		[Test]
		public void Delete_PassValidId_DeleteRecord()
		{
			var brand = CreateBrand();
			var res = _httpClient.Delete($"/brand/delete/{brand.Id}");
			Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
			Assert.IsNull(_unitOfWork.BrandRepo.Get(brand.Id));
		}
		[Test]
		public void Delete_PassInvalidId_ReturnNotFound()
		{
			var fakeId = Guid.NewGuid().ToString();
			var res = _httpClient.Delete($"/brand/delete/{fakeId}");
			Assert.AreEqual(HttpStatusCode.NotFound, res.StatusCode);
		}
		#region PrivateMethods
		private BrandDTO CreateBrand()
		{
			var brand = new Brand()
			{
				Name = "test",
				ImagePath = "no image",
				IsActive = true,
			};
			_unitOfWork.BrandRepo.Add(brand);
			_unitOfWork.Save();
			return _mapper.Map<BrandDTO>(brand);
		}

		#endregion
	}
}
