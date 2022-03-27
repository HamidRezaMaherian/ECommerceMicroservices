using Order.Domain.Common;

namespace Order.Domain.Entities;

public class Brand : EntityBase<string>
{
	public string Name { get; set; }
	public string ImagePath { get; set; }

	#region NavigationProps
	public IReadOnlyCollection<Product> Products { get; set; }
	#endregion
}