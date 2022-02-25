using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Common;
using Product.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Infrastructure.Persist.DAOs
{
	public class ProductPropertyDAO : ProductProperty, IEntityTypeConfiguration<ProductPropertyDAO>
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public override string Id { get; set; }

		[Required]
		[MaxLength(500)]
		public override string Value { get; set; }
		[Required]
		public override string PropertyId { get; set; }
		[Required]
		public override string ProductId { get; set; }

		#region NavigationProps
		public new ProductDAO Product { get; set; }
		public new PropertyDAO Property { get; set; }

		public void Configure(EntityTypeBuilder<ProductPropertyDAO> builder)
		{
			builder.HasKey(c => new { c.ProductId, c.PropertyId });
		}
		#endregion
	}
}
