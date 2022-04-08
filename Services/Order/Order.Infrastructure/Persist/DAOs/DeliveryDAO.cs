using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Order.Infrastructure.Persist.DAOs
{
	public class DeliveryDAO : EntityPrimaryBaseDAO<string>, IEntityTypeConfiguration<DeliveryDAO>
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
		[Required]
		public DeliveryStatus Status { get; set; }

		#region Relations
		[Required]
		public string OrderId { get; set; }
		#endregion

		public void Configure(EntityTypeBuilder<DeliveryDAO> builder)
		{
			builder.HasOne<OrderDAO>().WithOne(i => i.Delivery).HasForeignKey<DeliveryDAO>(i => i.OrderId);
		}
	}
}