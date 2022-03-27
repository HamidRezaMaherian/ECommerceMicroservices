using System.ComponentModel.DataAnnotations;

namespace Order.Infrastructure.Persist.DAOs;

public class BrandDAO : EntityBaseDAO<string>
{
	[Required]
	[MaxLength(150)]
	public string Name { get; set; }
	#region NavigationProps
	public IReadOnlyCollection<ProductDAO> Products { get; set; }
	#endregion
}