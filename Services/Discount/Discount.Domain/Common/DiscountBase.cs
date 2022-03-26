namespace Discount.Domain.Common
{
	public abstract class DiscountBase : EntityBase<string>
	{
		public DateTime StartDateTime { get; set; }
		public DateTime EndDateTime { get; set; }
		public string ProductId { get; set; }
		public string StoreId { get; set; }
	}
}
