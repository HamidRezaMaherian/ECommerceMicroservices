using Product.Infrastructure.Persist.DAOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Infrastructure.Persist.DAOs
{
	public class ProductDAO : EntityBase<string>
	{
		[MaxLength(300)]
		[Required]
		public string Name { get; set; }
		[Required]
		[MaxLength(500)]
		public string ShortDesc { get; set; }
		[Required]
		public string Description { get; set; }

		[Required]
		public decimal UnitPrice { get; set; }

		[Required]
		public string MainImagePath { get; set; }

		public DateTime CreatedDateTime { get; set; }

		public string CategoryId { get; set; }
		#region Relations
		[ForeignKey(nameof(CategoryId))]
		public ProductCategoryDAO Category { get; set; }
		public ICollection<ProductImageDAO> Images { get; set; }
		#endregion
	}
}
