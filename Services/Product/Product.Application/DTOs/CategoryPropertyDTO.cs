using Product.Domain.Common;

namespace Product.Application.DTOs;
public class CategoryPropertyDTO
{
	public string Id { get; set; }
	public virtual string PropertyId { get; set; }
	public virtual string CategoryId { get; set; }
	#region NavigationProps
	public virtual ProductCategoryDTO Category { get; set; }
	public virtual PropertyDTO Property { get; set; }
	#endregion
}
