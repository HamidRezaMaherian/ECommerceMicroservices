using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Product.API.Controllers;
using Product.Application.DTOs;
using Product.Application.Services;
using Services.Shared.Tests;
using System.Collections.Generic;
using System.Linq;

namespace Product.API.Tests.Unit
{
	[TestFixture]
	public class ProductControllerTests
	{
		private IProductService _productService;
		private ProductController _productController;
		private IList<Domain.Entities.Product> _products;
		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_products ??= new List<Domain.Entities.Product>();
			_productService = MockAction<Domain.Entities.Product, ProductDTO>
				.MockServie<IProductService>(_products).Object;
			_productController = new ProductController(_productService);
		}
		[TearDown]
		public void TearDown()
		{
			_products.Clear();
		}
		[Test]
		public void GetAll_ReturnAllProducts()
		{
			_productService.Add(new()
			{
				Id = "1",
				Name = "Name",
			});
			var res = _productController.GetAll();
			Assert.AreEqual(res.Value?.Count(), 1);
		}
		[Test]
		public void Create_AddProduct()
		{
			var product = new ProductDTO()
			{
				Id = "1",
				Name = "Name",
			};
			var result = _productController.Create(product);
			Assert.AreEqual(_products.Count, 1);
		}
	}
}
