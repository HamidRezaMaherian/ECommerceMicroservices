using Product.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Product.Infrastructure.Persist.DAOs
{
	public class PropertyDAO : EntityBaseDAO<string>
	{
		[MaxLength(300)]
		[Required]
		public string Name { get; set; }
		public PropertyType Type { get; set; }
	}
}
