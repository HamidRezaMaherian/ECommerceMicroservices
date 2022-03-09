using Services.Shared.Common;

namespace Product.Application.DTOs;
public class ProductPropertyDTO : EntityBase<string>
{
	public virtual string Value { get; set; }
	public virtual string PropertyId { get; set; }
	public virtual string ProductId { get; set; }

	#region NavigationProps
	public ProductDTO Product { get; set; }
	public PropertyDTO Property { get; set; }
	#endregion
}
