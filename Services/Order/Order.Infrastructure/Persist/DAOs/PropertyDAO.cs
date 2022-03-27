using System.ComponentModel.DataAnnotations;

namespace Order.Infrastructure.Persist.DAOs
{
	public class PropertyDAO : EntityBaseDAO<string>
	{
		[MaxLength(300)]
		[Required]
		public string Name { get; set; }
	}
}
