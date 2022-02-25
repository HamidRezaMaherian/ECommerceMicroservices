using Product.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Infrastructure.Persist.DAOs
{
	public class ProductCategoryDAO : ProductCategory
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public override string Id { get; set; }
		[MaxLength(150)]
		[Required]
		public override string Name { get; set; }
		#region Relations
		[ForeignKey(nameof(ParentId))]
		public new ProductCategoryDAO Parent { get; set; }
		#endregion
	}
}
