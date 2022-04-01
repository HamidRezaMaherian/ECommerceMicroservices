using System.ComponentModel.DataAnnotations;

namespace Order.Infrastructure.Persist.DAOs
{
	public class DeliveryDAO : EntityPrimaryBaseDAO<string>
	{
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		[Required]
		public string EmailAddress { get; set; }
		[Required]
		public string Address { get; set; }
		[Required]
		public string State { get; set; }
		[Required]
		public string ZipCode { get; set; }
		[Required]
		public DateTime DeliverDateTime { get; set; }
		[Required]
		public uint DeliverPrice { get; set; }
	}
}