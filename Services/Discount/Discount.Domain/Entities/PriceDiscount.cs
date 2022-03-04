using Discount.Domain.Common;

namespace Discount.Domain.Entities
{
	public class PriceDiscount : DiscountBase
	{
		//[Required]
		public decimal Price { get; set; }
	}
}
