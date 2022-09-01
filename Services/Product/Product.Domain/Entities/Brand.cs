using Product.Domain.Common;
using Product.Domain.ValueObjects;

namespace Product.Domain.Entities;

public class Brand : EntityBase<string>
{
	public string Name { get; set; }
	public Blob Image { get; set; }

	#region NavigationProps
	public IReadOnlyCollection<Product> Products { get; set; }
	#endregion
}