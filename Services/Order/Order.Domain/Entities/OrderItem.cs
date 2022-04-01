using Order.Domain.Common;

namespace Order.Domain.Entities
{
	public class OrderItem : EntityBase<string>
	{
		public string ProductId { get; set; }
		public string PropertyId { get; set; }
		public uint Count { get; set; }
		public uint Price { get; set; }
	}
}
