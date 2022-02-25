using Product.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Infrastructure.Persist.DAOs
{
	public class ProductImageDAO : ProductImage
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public override string Id { get; set; }

		[Required]
		public override string ImagePath { get; set; }
		#region Relations
		[Required]
		public override string ProductId { get; set; }
		[ForeignKey(nameof(ProductId))]
		public new virtual ProductDAO Product { get; set; }
		#endregion
	}
}
