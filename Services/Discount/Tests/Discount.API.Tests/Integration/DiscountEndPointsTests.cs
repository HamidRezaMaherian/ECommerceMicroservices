using Discount.API.Tests.Utils;
using Discount.Application.Configurations;
using Discount.Application.DTOs;
using Discount.Application.Services;
using Discount.Application.UnitOfWork;
using Discount.Domain.Common;
using Discount.Domain.Entities;
using Discount.Infrastructure.Persist;
using Discount.Infrastructure.Persist.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Services.Shared.APIUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;

namespace Discount.API.Tests.Integration
{
	[TestFixture]
	public class DiscountEndPointsTests
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
			var percentDiscounts = _unitOfWork.PercentDiscountRepo.Get();
			foreach (var item in percentDiscounts)
			{
				_unitOfWork.PercentDiscountRepo.Delete(item);
			}
			var priceDiscounts = _unitOfWork.PriceDiscountRepo.Get();
			foreach (var item in priceDiscounts)
			{
				_unitOfWork.PriceDiscountRepo.Delete(item);
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
		public void GetAll_ReturnAllDiscounts()
		{
			var productId = Guid.NewGuid().ToString();
			_unitOfWork.PriceDiscountRepo.Add(new PriceDiscount()
			{
				StartDateTime = DateTime.Now,
				EndDateTime = DateTime.Now,
				IsActive = true,
				ProductId = productId,
				Price = 40,
				StoreId = Guid.NewGuid().ToString()
			});
			_unitOfWork.PercentDiscountRepo.Add(new PercentDiscount()
			{
				StartDateTime = DateTime.Now,
				EndDateTime = DateTime.Now,
				IsActive = true,
				ProductId = productId,
				Percent = 40,
				StoreId = Guid.NewGuid().ToString()
			});
			_unitOfWork.Save();
			var res = _httpClient.Get<IEnumerable<object>>($"/discount/all/{productId}");

			CollectionAssert.AreEquivalent(res.Select(i => ((JsonElement)i).GetProperty("id").GetString()),
				Enumerable.Concat<DiscountBase>(
				_unitOfWork.PercentDiscountRepo.Get(),
				_unitOfWork.PriceDiscountRepo.Get())
				.Select(i => i.Id)
				);
		}
		[Test]
		public void AddPercentDiscount_PassValidObject_AddObject()
		{
			var discount = new PercentDiscountDTO()
			{
				StartDateTime = DateTime.Now,
				EndDateTime = DateTime.Now,
				IsActive = true,
				ProductId = Guid.NewGuid().ToString(),
				Percent = 40,
				StoreId = Guid.NewGuid().ToString()
			};
			var res = _httpClient.Post("/discount/percent", discount);

			Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);

			Assert.IsTrue(_unitOfWork.PercentDiscountRepo.Exists(i => i.Percent == discount.Percent));
		}
		[Test]
		public void AddPercentDiscount_PassInvalidObject_ReturnBadRequest()
		{
			var discount = new PercentDiscountDTO() { ProductId = Guid.NewGuid().ToString() };

			var res = _httpClient.Post("/discount/percent", discount);

			Assert.AreEqual(HttpStatusCode.BadRequest, res.StatusCode);
			Assert.IsFalse(_unitOfWork.PercentDiscountRepo.Exists(i => i.ProductId == discount.ProductId));
		}
		public void AddPriceDiscount_PassValidObject_AddObject()
		{
			var discount = new PriceDiscountDTO()
			{
				StartDateTime = DateTime.Now,
				EndDateTime = DateTime.Now,
				IsActive = true,
				ProductId = Guid.NewGuid().ToString(),
				Price = 40,
				StoreId = Guid.NewGuid().ToString()
			};
			var res = _httpClient.Post("/discount/percent", discount);

			Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);

			Assert.IsTrue(_unitOfWork.PriceDiscountRepo.Exists(i => i.Price == discount.Price));
		}
		[Test]
		public void AddPriceDiscount_PassInvalidObject_ReturnBadRequest()
		{
			var discount = new PriceDiscountDTO() { ProductId = Guid.NewGuid().ToString() };

			var res = _httpClient.Post("/discount/percent", discount);

			Assert.AreEqual(HttpStatusCode.BadRequest, res.StatusCode);
			Assert.IsFalse(_unitOfWork.PriceDiscountRepo.Exists(i => i.ProductId == discount.ProductId));
		}
	}
}
