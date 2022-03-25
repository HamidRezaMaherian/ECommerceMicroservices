using Discount.API.Controllers;
using Discount.Application.DTOs;
using Discount.Application.Services;
using Discount.Domain.Common;
using Discount.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using static Services.Shared.Tests.TestUtilsExtension;

namespace Discount.API.Tests.Unit
{
	[TestFixture]
	public class ProductCategoryControllerTests
	{
		private IPercentDiscountService _percentDiscountService;
		private IDiscountBaseService _discountBaseService;
		private IPriceDiscountService _priceDiscountService;
		private DiscountController _discountController;
		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			IList<DiscountBase> _discounts = new List<DiscountBase>();
			_percentDiscountService = MockAction<PercentDiscount, PercentDiscountDTO>
				.MockServie<IPercentDiscountService>(_discounts.Cast<PercentDiscount>().ToList())
				.Object;
			_priceDiscountService = MockAction<PriceDiscount, PriceDiscountDTO>
				.MockServie<IPriceDiscountService>(_discounts.Cast<PriceDiscount>().ToList())
				.Object;
			_discountBaseService = TestUtils.MockDiscountBaseService(_discounts);

			_discountController = new DiscountController(
				_percentDiscountService,
				_priceDiscountService,
				_discountBaseService);
		}
		[Test]
		public void GetActiveDiscounts_ProductExist_ReturnDiscountsList()
		{
			_percentDiscountService.Add(new()
			{
				ProductId = "testId",
				StartDateTime = DateTime.Now,
				EndDateTime = DateTime.Now.AddDays(2),
				Percent = 10
			});

			_priceDiscountService.Add(new()
			{
				ProductId = "testId",
				StartDateTime = DateTime.Now,
				EndDateTime = DateTime.Now.AddDays(2),
				Price = 23000
			});

			var res = _discountController.GetActiveDiscounts("testId");

			Assert.That(res.Value?.Select(i => i.Id),
				Is.EquivalentTo(_discountBaseService.GetAll().Select(i => i.Id)));
		}
		[Test]
		public void AddPercentDiscount_ReturnOk()
		{
			var percentDiscunt = new PercentDiscountDTO()
			{
				ProductId = "testId",
				StartDateTime = DateTime.Now,
				EndDateTime = DateTime.Now.AddDays(2),
				Percent = 10
			};
			var res = _discountController.AddPercentDiscount(percentDiscunt);

			Assert.IsNotNull(_percentDiscountService.GetById(percentDiscunt.Id));
		}
		[Test]
		public void AddPriceDiscount_ReturnOk()
		{
			var priceDiscount = new PriceDiscountDTO()
			{
				ProductId = "testId",
				StartDateTime = DateTime.Now,
				EndDateTime = DateTime.Now.AddDays(2),
				Price = 10
			};
			var res = _discountController.AddPriceDiscount(priceDiscount);

			Assert.IsNotNull(_priceDiscountService.GetById(priceDiscount.Id));
		}

		private static class TestUtils
		{
			public static IDiscountBaseService MockDiscountBaseService(IList<DiscountBase> list)
			{
				var store = new Mock<IDiscountBaseService>();
				store.Setup(i => i.GetAll()).Returns(list.ToList());
				store.Setup(i => i.GetAll(It.IsAny<Expression<Func<DiscountBase, bool>>>()))
					.Returns(list.ToList());
				return store.Object;
			}
		}
	}
}
