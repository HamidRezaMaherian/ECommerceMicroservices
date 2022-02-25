using Product.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Product.Domain.Entities
{
	public class ProductProperty : EntityBase<string>
	{
		public virtual string Value { get; set; }
		public virtual string PropertyId { get; set; }
		public virtual string ProductId { get; set; }

		#region NavigationProps
		public Product Product { get; set; }
		public Property Property { get; set; }
		#endregion
	}
}
