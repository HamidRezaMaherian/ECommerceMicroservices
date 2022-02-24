using Product.Domain.Common;

namespace Discount.Domain.Common
{
	public abstract class DiscountBase : IBaseDelete
	{
		public DateTime StartDateTime { get; set; }
		public DateTime EndDateTime { get; set; }

		#region Relations
		//[Required]
		//[Key]
		public string ProductId { get; set; }

		//[ForeignKey(nameof(ProductId))]
		//public virtual Product Product { get; set; }
		#endregion
		public bool IsDelete { get; set; }
	}
}
