using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Infrastructure.Persist.DAOs
{
	public class ProductCategoryDAO : EntityBaseDAO<string>
	{
		[MaxLength(150)]
		[Required]
		public string Name { get; set; }
		public string ParentId { get; set; }

		#region Relations
		[ForeignKey(nameof(ParentId))]
		public ProductCategoryDAO Parent { get; set; }
		#endregion
	}
}
