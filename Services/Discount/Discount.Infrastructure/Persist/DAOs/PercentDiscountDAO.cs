
using System.ComponentModel.DataAnnotations;

namespace Discount.Infrastructure.Persist.DAOs
{
	public class PercentDiscountDAO : EntityBaseDAO<string>
	{
		[Required]
		public DateTime StartDateTime { get; set; }
		[Required]
		public DateTime EndDateTime { get; set; }

		[Required]
		public string ProductId { get; set; }
		public string PropertyId { get; set; }
		[Required]
		public string StoreId { get; set; }

		[Range(1, 100)]
		[Required]
		public int Percent { get; set; }
	}
}
