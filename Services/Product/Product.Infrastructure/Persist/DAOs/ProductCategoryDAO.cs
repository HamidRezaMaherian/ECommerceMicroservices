using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Infrastructure.Persist.DAOs
{
	public class ProductCategoryDAO : EntityBase<string>
	{
		[MaxLength(150)]
		[Required]
		public string Name { get; set; }
		public string? ParentId { get; set; }

		#region Relations
		[ForeignKey(nameof(ParentId))]
		public ProductCategoryDAO Parent { get; set; }
		#endregion
	}
}
