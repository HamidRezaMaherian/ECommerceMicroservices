using Product.Domain.Common;
using Product.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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