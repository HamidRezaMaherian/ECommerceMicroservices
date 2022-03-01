using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace Product.Infrastructure.Persist.DAOs;

public class CategoryPropertyDAO : EntityFlagBase,
	IEntityTypeConfiguration<CategoryPropertyDAO>
{
	[Required]
	public virtual string PropertyId { get; set; }
	[Required]
	public virtual string CategoryId { get; set; }
	#region NavigationProps
	public virtual ProductCategoryDAO Category { get; set; }
	public virtual PropertyDAO Property { get; set; }
	#endregion
	public void Configure(EntityTypeBuilder<CategoryPropertyDAO> builder)
	{
		builder.HasKey(c => new { c.CategoryId, c.PropertyId });
	}
}
