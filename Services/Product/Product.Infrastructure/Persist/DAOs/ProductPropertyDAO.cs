using System.ComponentModel.DataAnnotations;

namespace Product.Infrastructure.Persist.DAOs
{
	public class ProductBasePropertyDAO : EntityBaseDAO<string>
	{
		[Required]
		[MaxLength(500)]
		public string Value { get; set; }
		[Required]
		public string PropertyId { get; set; }
		[Required]
		public string ProductId { get; set; }

		#region NavigationProps
		public virtual ProductDAO Product { get; set; }
		public virtual PropertyDAO Property { get; set; }
		#endregion
	}

	public class ProductPropertyDAO : ProductBasePropertyDAO
	{
	}
	public class ProductPricePropertyDAO : ProductBasePropertyDAO
	{
		[Required]
		public double Price { get; set; }
	}
}
