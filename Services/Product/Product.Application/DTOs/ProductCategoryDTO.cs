using Services.Shared.Common;

namespace Product.Application.DTOs;
public class ProductCategoryDTO : EntityBase<string>
{
	public virtual string Name { get; set; }

	#region Relations
	public string? ParentId { get; set; }
	public ProductCategoryDTO Parent { get; set; }
	#endregion
}

