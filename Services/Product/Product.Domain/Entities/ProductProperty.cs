using Product.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Product.Domain.Entities
{
	public class ProductProperty : EntityBase<string>
	{
		//[Required]
		//[MaxLength(500)]
		public string Value { get; set; }
		//[Required]
		public string PropertyId { get; set; }
		//[Required]
		public string ProductId { get; set; }

		#region NavigationProps
		public virtual Product Product { get; set; }
		public virtual Property Property { get; set; }
		#endregion
	}
}
