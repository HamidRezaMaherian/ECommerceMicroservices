namespace Order.Infrastructure.Persist.DAOs
{
	public class OrderDAO : EntityBaseDAO<string>
	{
		public string UserName { get; set; }
		public uint TotalPrice
		{
			get
			{
				return ((uint)Items.Sum(x => x.Price)) + (Delivery?.DeliverPrice ?? 0);
			}
		}
		public ICollection<OrderItemDAO> Items { get; set; }
		// Payment
		public PaymentDAO Payment { get; set; }
		public DeliveryDAO Delivery { get; set; }
	}
}
