using Product.Domain.Common;

namespace Product.Domain.Entities
{
	public class CategoryProperty : EntityFlagBase
	{
		//[Required]
		public string PropertyId { get; set; }
		//[Required]
		public string CategoryId { get; set; }
		#region NavigationProps
		public virtual ProductCategory Category { get; set; }
		public virtual Property Property { get; set; }
		#endregion
	}
}
