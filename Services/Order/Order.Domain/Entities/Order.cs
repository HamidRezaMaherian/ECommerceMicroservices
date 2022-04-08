using Order.Domain.Common;

namespace Order.Domain.Entities
{
	public class Order : EntityBase<string>
	{
		public string UserName { get; set; }
		public uint TotalPrice
		{
			get
			{
				return ((uint)Items.Sum(x => x.Price)) + (Delivery?.DeliverPrice ?? 0);
			}
		}
		public IReadOnlyCollection<OrderItem> Items { get; set; }
		// Payment
		public Payment Payment { get; set; }
		public Delivery Delivery { get; set; }
	}
}
