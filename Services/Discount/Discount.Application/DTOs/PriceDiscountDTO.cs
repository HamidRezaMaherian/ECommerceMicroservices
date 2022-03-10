using Services.Shared.Common;

namespace Discount.Domain.Entities
{
	public class PriceDiscountDTO : EntityBase<string>
	{
		public DateTime StartDateTime { get; set; }
		public DateTime EndDateTime { get; set; }
		public string ProductId { get; set; }
		public string StoreId { get; set; }
		public decimal Price { get; set; }
	}
}
