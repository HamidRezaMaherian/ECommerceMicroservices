using Product.Domain.Common;

namespace Product.Domain.Entities;

public class Brand : EntityBase<string>
{
	//[MaxLength(150)]
	//[Required]
	public string Name { get; set; }
	public string ImagePath { get; set; }

	#region NavigationProps
	public virtual Product Products { get; set; }
	#endregion
}