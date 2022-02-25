using Product.Infrastructure.Persist.DAOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Infrastructure.Persist.DAOs
{
	public class ProductDAO : Domain.Entities.Product
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public override string Id { get; set; }

		[MaxLength(300)]
		[Required]
		public override string Name { get; set; }
		[Required]
		[MaxLength(500)]
		public override string ShortDesc { get; set; }
		[Required]
		public override string Description { get; set; }

		[Required]
		public override decimal UnitPrice { get; set; }

		[Required]
		public override string MainImagePath { get; set; }
		#region Relations
		[ForeignKey(nameof(CategoryId))]
		public new ProductCategoryDAO Category { get; set; }
		public new ICollection<ProductImageDAO> Images { get; set; }
		#endregion
	}
}
