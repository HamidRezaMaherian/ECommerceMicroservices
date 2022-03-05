using Discount.Application.Services;
using Discount.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class DiscountController : ControllerBase
	{
		private readonly IPercentDiscountService _percentDiscountService;
		private readonly IPriceDiscountService _priceDiscountService;

		public DiscountController(IPercentDiscountService percentDiscountService, IPriceDiscountService priceDiscountService)
		{
			_percentDiscountService = percentDiscountService;
			_priceDiscountService = priceDiscountService;
		}

		public ActionResult<DiscountBase> GetActiveDiscounts(object productId)
		{
			throw new NotImplementedException();
		}
	}
}