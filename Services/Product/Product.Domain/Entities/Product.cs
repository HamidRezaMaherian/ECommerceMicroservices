using Product.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Domain.Entities
{
	public class Product : EntityBase<string>
	{
		//[MaxLength(300)]
		//[Required]
		public string Name { get; set; }
		//[Required]
		//[MaxLength(500)]
		public string ShortDesc { get; set; }
		//[Required]
		public string Description { get; set; }

		//[Required]
		public decimal UnitPrice { get; set; }

		//[Required]
		public string MainImagePath { get; set; }

		public DateTime CreatedDateTime { get; set; }

		//[Required]
		public string CategoryId { get; set; }
		#region Relations
		[ForeignKey(nameof(CategoryId))]
		public virtual ProductCategory Category { get; set; }
		public virtual ProductStock Stock { get; set; }
		public virtual ICollection<ProductImage> Images { get; set; }
		//public virtual ProductDiscount Discount { get; set; }
		#endregion
	}
}
