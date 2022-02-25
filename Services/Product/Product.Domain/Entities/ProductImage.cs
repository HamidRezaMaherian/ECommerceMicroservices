using Product.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Domain.Entities
{
	public class ProductImage : EntityBase<string>
	{
		public virtual string ImagePath { get; set; }
		#region Relations
		public virtual string ProductId { get; set; }
		public Product Product { get; set; }
		#endregion
	}
}
