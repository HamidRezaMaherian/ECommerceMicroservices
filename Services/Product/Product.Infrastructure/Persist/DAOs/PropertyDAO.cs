using Product.Domain.Entities;
using Product.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Infrastructure.Persist.DAOs
{
	public class PropertyDAO : Property
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public override string Id { get; set; }
		[MaxLength(300)]
		[Required]
		public override string Name { get; set; }
		public new PropertyType Type { get; set; }
	}
}
