using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace Order.Infrastructure.Persist.DAOs
{
	public class OrderItemDAO : EntityBaseDAO<string>, IEntityTypeConfiguration<OrderItemDAO>
	{
		[Required]
		public string ProductId { get; set; }
		[Required]
		public string PropertyId { get; set; }
		[Required]
		public uint Count { get; set; }
		[Required]
		public uint Price { get; set; }
		#region Relations
		[Required]
		public string OrderId { get; set; }
		#endregion
		public void Configure(EntityTypeBuilder<OrderItemDAO> builder)
		{
			builder.HasOne<OrderDAO>().WithMany(i => i.Items).HasForeignKey(i => i.OrderId);
		}
	}
}
