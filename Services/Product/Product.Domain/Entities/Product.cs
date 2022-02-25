using Product.Domain.Common;

namespace Product.Domain.Entities
{
	public class Product : EntityBase<string>
	{
		public virtual string Name { get; set; }
		public virtual string ShortDesc { get; set; }
		public virtual string Description { get; set; }

		public virtual decimal UnitPrice { get; set; }

		public virtual string MainImagePath { get; set; }

		public virtual DateTime CreatedDateTime { get; set; }

		public string CategoryId { get; set; }
		#region Relations
		public virtual ProductCategory Category { get; set; }
		public virtual ICollection<ProductImage> Images { get; set; }
		#endregion
	}
}
