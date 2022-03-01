using Product.Domain.Common;

namespace Product.Domain.Entities
{
	public class ProductCategory : EntityBase<string>
	{
		public string Name { get; set; }

		#region Relations
		public string? ParentId { get; set; }
		public ProductCategory Parent { get; set; }
		#endregion
	}
}
