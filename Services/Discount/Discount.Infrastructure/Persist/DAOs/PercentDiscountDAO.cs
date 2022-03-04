
using System.ComponentModel.DataAnnotations;

namespace Discount.Infrastructure.Persist.DAOs
{
	public class PercentDiscountDAO : EntityBase<string>
	{
		[Required]
		public DateTime StartDateTime { get; set; }
		[Required]
		public DateTime EndDateTime { get; set; }

		[Required]
		public string ProductId { get; set; }

		[Range(1, 100)]
		[Required]
		public int Percent { get; set; }
	}
}
