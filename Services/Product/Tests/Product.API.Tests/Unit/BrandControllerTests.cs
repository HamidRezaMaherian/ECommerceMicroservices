using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Product.API.Controllers;
using Product.Application.DTOs;
using Product.Application.Services;
using Product.Domain.Entities;
using Services.Shared.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using static Services.Shared.Tests.TestUtilsExtension;

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
			_brandService = MockAction<Brand, BrandDTO>
				.MockServie<IBrandService>(_brands).Object;
			_brandController = new BrandController(_brandService);
		}
		[Test]
		public void GetAll_ReturnAllProducts()
		{
			var brand = new BrandDTO()
			{
				Id = Guid.NewGuid().ToString(),
				Name = "Name",
			};
			_brandService.Add(brand);
			var res = _brandController.GetAll();
			CollectionAssert.AreEquivalent(res.Value?.Select(i=>i.Id), _brandService.GetAll().Select(i=>i.Id));
		}
		[Test]
		public void Create_AddBrand()
		{
			var brand = new BrandDTO()
			{
				Id = Guid.NewGuid().ToString(),
				Name = "Name",
			};
			var result = _brandController.Create(brand);
			Assert.IsNotNull(_brandService.GetById(brand.Id));
		}
	}
}
