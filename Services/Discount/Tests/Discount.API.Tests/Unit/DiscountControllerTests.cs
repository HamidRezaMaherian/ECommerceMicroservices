using Discount.API.Controllers;
using Discount.API.Tests.Utils;
using Discount.Application.DTOs;
using Discount.Application.Services;
using Discount.Domain.Common;
using Discount.Domain.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Discount.API.Tests.Unit
{
	[TestFixture]
	public class ProductCategoryControllerTests
	{
		private IPercentDiscountService _percentDiscountService;
		private IPriceDiscountService _priceDiscountService;
		private DiscountController _discountController;
		private IList<DiscountBase> _discounts;
		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_discounts ??= new List<DiscountBase>();
			_percentDiscountService = MockAction<PercentDiscount, PercentDiscountDTO>
				.MockServie<IPercentDiscountService>(_discounts.Cast<PercentDiscount>().ToList())
				.Object;
			_priceDiscountService = MockAction<PriceDiscount, PriceDiscountDTO>
				.MockServie<IPriceDiscountService>(_discounts.Cast<PriceDiscount>().ToList())
				.Object;
			_discountController = new DiscountController(_percentDiscountService,_priceDiscountService);
		}
		[TearDown]
		public void TearDown()
		{
			_discounts.Clear();
		}
		[Test]
		public void GetActiveDiscounts_ProductExist_ReturnDiscountsList()
		{

			//Assert.AreEqual(_discounts.Count, 1);
		}
		[Test]
		public void GetActiveDiscounts_ProductNot_ReturnNotFound()
		{
			//_discountController.Create(productCategory);
			//Assert.AreEqual(_discounts.Count, 1);
		}
	}
}
