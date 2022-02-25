using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Infrastructure.Persist.DAOs
{
	public class CategoryPropertyDAO : CategoryProperty,IEntityTypeConfiguration<CategoryPropertyDAO>
	{
		[Required]
		public override string PropertyId { get; set; }
		[Required]
		public override string CategoryId { get; set; }
		public void Configure(EntityTypeBuilder<CategoryPropertyDAO> builder)
		{
			builder.HasKey(c => new { c.CategoryId, c.PropertyId });
		}
	}
}
