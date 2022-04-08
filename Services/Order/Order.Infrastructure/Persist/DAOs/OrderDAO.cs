namespace Order.Infrastructure.Persist.DAOs
{
	public class OrderDAO : EntityBaseDAO<string>
	{
		public string UserName { get; set; }
		public ICollection<OrderItemDAO> Items { get; set; }
		// Payment
		public PaymentDAO Payment { get; set; }
		public DeliveryDAO Delivery { get; set; }
	}
}
