namespace Discount.Application.DTOs
{
	public class PercentDiscountDTO
	{
		public DateTime StartDateTime { get; set; }
		public DateTime EndDateTime { get; set; }
		public string ProductId { get; set; }
		public int Percent { get; set; }
	}
}
