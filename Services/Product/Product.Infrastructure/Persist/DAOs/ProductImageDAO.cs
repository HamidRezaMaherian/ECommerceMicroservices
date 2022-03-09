using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Infrastructure.Persist.DAOs
{
	public class ProductImageDAO : EntityBaseDAO<string>
	{
		[Required]
		public string ImagePath { get; set; }
		#region Relations
		[Required]
		public string ProductId { get; set; }
		[ForeignKey(nameof(ProductId))]
		public virtual ProductDAO Product { get; set; }

		#endregion
	}
}
