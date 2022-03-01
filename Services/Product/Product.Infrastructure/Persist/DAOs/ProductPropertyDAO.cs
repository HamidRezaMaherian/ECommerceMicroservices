using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Common;
using Product.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Infrastructure.Persist.DAOs
{
	public class ProductPropertyDAO : EntityBase<string>,
		IEntityTypeConfiguration<ProductPropertyDAO>
	{
		[Required]
		[MaxLength(500)]
		public string Value { get; set; }
		[Required]
		public string PropertyId { get; set; }
		[Required]
		public string ProductId { get; set; }

		#region NavigationProps
		public virtual ProductDAO Product { get; set; }
		public virtual PropertyDAO Property { get; set; }

		public void Configure(EntityTypeBuilder<ProductPropertyDAO> builder)
		{
			builder.HasKey(c => new { c.ProductId, c.PropertyId });
		}
		#endregion
	}
}
