using Product.Domain.Common;

namespace Product.Domain.Entities
{
	public class ProductImage : EntityBase<string>
	{
		public string ImagePath { get; set; }
		#region Relations
		public string ProductId { get; set; }
		public Product Product { get; set; }
		#endregion
	}
}
