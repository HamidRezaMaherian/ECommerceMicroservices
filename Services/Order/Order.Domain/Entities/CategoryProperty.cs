using Order.Domain.Common;

namespace Order.Domain.Entities
{
	public class CategoryProperty : EntityPrimaryBase<string>
	{
		public string PropertyId { get; set; }
		public string CategoryId { get; set; }
		#region NavigationProps
		public ProductCategory Category { get; set; }
		public Property Property { get; set; }
		#endregion
	}
}
