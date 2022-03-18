using NUnit.Framework;
using Product.Infrastructure.Persist;
using Product.Infrastructure.Persist.DAOs;
using Product.Infrastructure.Repositories;
using Product.Infrastructure.Tests.Utils;
using Services.Shared.AppUtils;
using Services.Shared.Tests;
using System;
using System.Linq;

namespace Product.Infrastructure.Tests.Unit.Repositories
{
	[TestFixture]
	public class ProductServiceTests
	{
		private ProductRepo _productRepo;
		private ApplicationDbContext _db;
		[OneTimeSetUp]
		public void OneTimeSetup()
		{
			_db?.Dispose();
			_db = MockActions.MockDbContext("TestDb");
			var mapper = UtilsExtension.CreateMapper<Domain.Entities.Product, ProductDAO>();
			_productRepo = new ProductRepo(_db, mapper);
		}

		[Test]
		public void Add_CreateEntity()
		{
			var product = new Domain.Entities.Product()
			{
				Name = "Test",
				CreatedDateTime = DateTime.Now,
				ShortDesc = "no desc",
				IsActive = true,
				Description = "no desc",
				UnitPrice = 45000,
				MainImagePath = "no image"
			};
			_productRepo.Add(product);
			Assert.AreEqual(_db.Products.Count(i => i.Id == product.Id), 1);
		}
		[Test]
		public void GetAll_WithValidCondition_ReturnEntities()
		{
			var product = new Domain.Entities.Product()
			{
				Name = "Test",
				CreatedDateTime = DateTime.Now,
				ShortDesc = "no desc",
				IsActive = true,
				Description = "no desc",
				UnitPrice = 45000,
				MainImagePath = "no image"
			};
			_productRepo.Add(product);

			var res = _productRepo.Get(new QueryParams<Domain.Entities.Product>()
			{
				Expression = i => i.Name == "Test"
			}).ToList();
			Assert.That(res.Count >= 1);
		}
	}
}
