using Product.Domain.Common;

namespace Product.Domain.Entities;

public class Brand : EntityBase<string>
{
	public virtual string Name { get; set; }
	public virtual string ImagePath { get; set; }

	#region NavigationProps
	public virtual IReadOnlyCollection<Product> Products { get; set; }
	#endregion
}