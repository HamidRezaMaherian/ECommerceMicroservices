
using System.ComponentModel.DataAnnotations;

namespace Discount.Infrastructure.Persist.DAOs
{
	public class PriceDiscountDAO : EntityBase<string>
	{
		[Required]
		public DateTime StartDateTime { get; set; }
		[Required]
		public DateTime EndDateTime { get; set; }

		[Required]
		public string ProductId { get; set; }

		public decimal Price { get; set; }
	}
}
