using Product.Domain.Common;
using Product.Domain.ValueObjects;

namespace Product.Domain.Entities
{
	public class ProductImage : EntityBase<string>
	{
		public Blob Image { get; set; }
		#region Relations
		public string ProductId { get; set; }
		public Product Product { get; set; }
		#endregion
	}
}
