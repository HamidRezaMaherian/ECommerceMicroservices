using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Order.Infrastructure.Persist.DAOs
{
	public class PaymentDAO : IEntityTypeConfiguration<PaymentDAO>
	{
		[Required]
		public PaymentStatus Status { get; set; }
		[Required]
		public PaymentMethod Method { get; set; }

		public string TransactionId { get; set; }

		#region Relations
		[Required]
		public string OrderId { get; set; }
		#endregion

		public void Configure(EntityTypeBuilder<PaymentDAO> builder)
		{
			builder.HasKey(x => x.OrderId);
			builder.HasOne<OrderDAO>().WithOne(i => i.Payment).HasForeignKey<PaymentDAO>(i => i.OrderId);
		}
	}
}