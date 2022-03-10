using NUnit.Framework;
using Product.Infrastructure.Persist;
using Product.Infrastructure.Persist.DAOs;
using Product.Infrastructure.Repositories;
using Product.Infrastructure.Tests.Utils;
using System;
using System.Linq;

namespace Product.Infrastructure.Tests.Unit.Repositories
{
	[TestFixture]
	public class ProductRepoTests
	{
		private ProductRepo _productRepo;
		private ApplicationDbContext _db;
		[OneTimeSetUp]
		public void OneTimeSetup()
		{
			_db?.Dispose();
			_db = MockActions.MockDbContext("TestDb");
			var mapper = MockActions.MockMapper(cfg =>
			{
				cfg.CreateMap<Domain.Entities.Product, ProductDAO>().ReverseMap();
			});
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
			_db.SaveChanges();
			Assert.AreEqual(_db.Products.Count(i=>i.Id==product.Id), 1);
		}
	}
}
