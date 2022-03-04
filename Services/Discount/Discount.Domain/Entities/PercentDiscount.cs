using Discount.Domain.Common;

namespace Discount.Domain.Entities
{
	public class PercentDiscount : DiscountBase
	{
		public int Percent { get; set; }
	}
}
