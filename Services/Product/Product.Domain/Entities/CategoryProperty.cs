using Product.Domain.Common;

namespace Product.Domain.Entities
{
	public class CategoryProperty
	{
		public string PropertyId { get; set; }
		public string CategoryId { get; set; }
		public bool IsActive { get; set; }
		#region NavigationProps
		public ProductCategory Category { get; set; }
		public Property Property { get; set; }
		#endregion
	}
}
