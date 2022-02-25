using Product.Domain.Common;
using Product.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Infrastructure.Persist.DAOs;

public class BrandDAO : Brand
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public override string Id { get; set; }
	[Required]
	[MaxLength(150)]
	public override string Name { get; set; }
}