using NUnit.Framework;
using Product.API.Controllers;
using Product.API.Configurations.DTOs;
using Product.Application.Services;
using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using static Product.API.Tests.Utils.TestUtilsExtension;

namespace Product.API.Tests.Unit
{
	[TestFixture]
	public class BrandControllerTests
	{
		private IBrandService _brandService;
		private BrandController _brandController;
		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			var _brands = new List<Brand>();
			_brandService = MockAction<Brand, Application.DTOs.BrandDTO>
				.MockServie<IBrandService>(_brands).Object;
			_brandController = new BrandController(_brandService);
		}
		[Test]
		public void GetAll_ReturnAllProducts()
		{
			var brand = new CreateBrandDTO()
			{
				Id = Guid.NewGuid().ToString(),
				Name = "Name",
				Image=""
			};
			_brandService.Add(brand);
			var res = _brandController.GetAll();
			CollectionAssert.AreEquivalent(res.Value?.Select(i => i.Id), _brandService.GetAll().Select(i => i.Id));
		}
		[Test]
		public void Create_AddBrand()
		{
			var brand = new CreateBrandDTO()
			{
				Id = Guid.NewGuid().ToString(),
				Name = "Name",
				Image="sdklf"
			};
			var result = _brandController.Create(brand);
			Assert.IsNotNull(_brandService.GetById(brand.Id));
		}
	}
}
