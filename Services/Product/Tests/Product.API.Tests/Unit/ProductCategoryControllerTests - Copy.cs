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

namespace Product.API.Tests.Unit
{
	[TestFixture]
	public class ProductCategoryControllerTests
	{
		private IProductCategoryService _productCategoryService;
		private ProductCategoryController _productCategoryController;
		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			var _productCategorys = new List<ProductCategory>();
			_productCategoryService = MockAction<ProductCategory, ProductCategoryDTO>
				.MockServie<IProductCategoryService>(_productCategorys).Object;
			_productCategoryController = new ProductCategoryController(_productCategoryService);
		}
		[Test]
		public void GetAll_ReturnAllProducts()
		{
			var productCategory = new ProductCategoryDTO()
			{
				Id = Guid.NewGuid().ToString(),
				Name = "Name",
			};
			_productCategoryService.Add(productCategory);
			var res = _productCategoryController.GetAll();
			CollectionAssert.AreEquivalent(res.Value?.Select(i=>i.Id), _productCategoryService.GetAll().Select(i=>i.Id));
		}
		[Test]
		public void Create_AddProductCategory()
		{
			var productCategory = new ProductCategoryDTO()
			{
				Id = Guid.NewGuid().ToString(),
				Name = "Name",
				IsActive = true	
			};
			var result = _productCategoryController.Create(productCategory);
			Assert.IsNotNull(_productCategoryService.GetById(productCategory.Id));
		}
	}
}
