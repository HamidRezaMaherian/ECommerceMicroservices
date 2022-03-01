using Product.Domain.Common;

namespace Product.Domain.Entities
{
	public class CategoryProperty : EntityPrimaryBase<string>
	{
		public virtual string PropertyId { get; set; }
		public virtual string CategoryId { get; set; }
		#region NavigationProps
		public virtual ProductCategory Category { get; set; }
		public virtual Property Property { get; set; }
		#endregion
	}
}
