using System.ComponentModel.DataAnnotations;

namespace Order.Infrastructure.Persist.DAOs
{
	public class OrderItemDAO : EntityBaseDAO<string>
	{
		[Required]
		public string ProductId { get; set; }
		[Required]
		public string PropertyId { get; set; }
		[Required]
		public uint Count { get; set; }
		[Required]
		public uint Price { get; set; }
	}
}
