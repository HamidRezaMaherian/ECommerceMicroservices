using System.ComponentModel.DataAnnotations;

namespace Product.Infrastructure.Persist.DAOs;

public class BrandDAO : EntityBaseDAO<string>
{
	[Required]
	[MaxLength(150)]
	public string Name { get; set; }

	public string ImagePath { get; set; }

	#region NavigationProps
	public IReadOnlyCollection<ProductDAO> Products { get; set; }
	#endregion
}