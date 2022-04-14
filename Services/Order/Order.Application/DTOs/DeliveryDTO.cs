using Order.Domain.Common;
using Order.Domain.Enums;

namespace Order.Application.DTOs
{
	public class DeliveryDTO : EntityPrimaryBase<string>
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string EmailAddress { get; set; }
		public string Address { get; set; }
		public string State { get; set; }
		public string ZipCode { get; set; }
		public DateTime DeliverDateTime { get; set; }
		public uint DeliverPrice { get; set; }
		public DeliveryStatus Status { get; set; }
	}
}