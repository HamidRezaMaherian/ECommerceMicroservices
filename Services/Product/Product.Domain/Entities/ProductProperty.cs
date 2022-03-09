using Services.Shared.Common;

namespace Product.Domain.Entities
{
	public class ProductProperty : EntityBase<string>
	{
		public string Value { get; set; }
		public string PropertyId { get; set; }
		public string ProductId { get; set; }

		#region NavigationProps
		public Product Product { get; set; }
		public Property Property { get; set; }
		#endregion
	}
}
