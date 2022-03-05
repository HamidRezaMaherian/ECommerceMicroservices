using NUnit.Framework;
using Product.API.Controllers;
using Product.API.Tests.Utils;
using Product.Application.DTOs;
using Product.Application.Services;
using Product.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Product.API.Tests.Unit
{
	[TestFixture]
	public class ProductCategoryControllerTests
	{
		private IProductCategoryService _productCategoryService;
		private ProductCategoryController _productCategoryController;
		private IList<ProductCategory> _productCategorys;
		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_productCategorys ??= new List<ProductCategory>();
			_productCategoryService = MockAction<ProductCategory, ProductCategoryDTO>
				.MockServie<IProductCategoryService>(_productCategorys).Object;
			_productCategoryController = new ProductCategoryController(_productCategoryService);
		}
		[TearDown]
		public void TearDown()
		{
			_productCategorys.Clear();
		}
		[Test]
		public void GetAll_ReturnAllProducts()
		{
			_productCategoryService.Add(new()
			{
				Id = "1",
				Name = "Name",
			});
			var res = _productCategoryController.GetAll();
			Assert.AreEqual(res.Value?.Count(), 1);
		}
		[Test]
		public void Create_AddProductCategory()
		{
			var productCategory = new ProductCategoryDTO()
			{
				Id = "1",
				Name = "Name",
				IsActive = true
			};
			_productCategoryController.Create(productCategory);
			Assert.AreEqual(_productCategorys.Count, 1);
		}
	}
}
