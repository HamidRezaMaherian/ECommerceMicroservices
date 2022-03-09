using Discount.Application.DTOs;
using Discount.Application.Services;
using Discount.Domain.Common;
using Discount.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class DiscountController : ControllerBase
	{
		private readonly IPercentDiscountService _percentDiscountService;
		private readonly IPriceDiscountService _priceDiscountService;
		private readonly IDiscountBaseService _discountBaseService;
		public DiscountController(IPercentDiscountService percentDiscountService, IPriceDiscountService priceDiscountService, IDiscountBaseService discountBaseService)
		{
			_percentDiscountService = percentDiscountService;
			_priceDiscountService = priceDiscountService;
			_discountBaseService = discountBaseService;
		}
		[HttpGet]
		public ActionResult<IEnumerable<DiscountBase>> GetActiveDiscounts(object productId)
		{
			return _discountBaseService.GetAll(i => i.ProductId == productId).ToList();
		}
		[HttpPost]
		public IActionResult AddPercentDiscount(PercentDiscountDTO discountDTO)
		{
			_percentDiscountService.Add(discountDTO);
			return Ok();
		}
		[HttpPost]
		public IActionResult AddPriceDiscount(PriceDiscountDTO discountDTO)
		{
			throw new NotImplementedException();
		}
	}
}