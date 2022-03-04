namespace Discount.Domain.Entities
{
	public class PriceDiscountDTO
	{
		public DateTime StartDateTime { get; set; }
		public DateTime EndDateTime { get; set; }
		public string ProductId { get; set; }
		public decimal Price { get; set; }
	}
}
