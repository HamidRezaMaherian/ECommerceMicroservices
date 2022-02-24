using Discount.Domain.Common;

namespace Discount.Domain.Entities
{
	public class ProductPriceDiscount : DiscountBase
	{
		//[Required]
		public decimal Price { get; set; }
	}
	public class ProductPercentDiscount : DiscountBase
	{
		//[Range(1, 100)]
		//[Required]
		public int Percent { get; set; }
	}
}
