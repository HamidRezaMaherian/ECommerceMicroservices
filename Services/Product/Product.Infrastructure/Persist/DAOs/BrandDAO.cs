using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace Product.Infrastructure.Persist.DAOs;

public class BrandDAO : EntityBase<string>
{
	[Required]
	[MaxLength(150)]
	public string Name { get; set; }
	#region NavigationProps
	public IReadOnlyCollection<ProductDAO> Products { get; set; }
	#endregion
}