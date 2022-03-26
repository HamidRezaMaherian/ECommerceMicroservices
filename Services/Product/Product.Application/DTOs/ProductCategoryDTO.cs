using Product.Domain.Common;

namespace Product.Application.DTOs;
public class ProductCategoryDTO : EntityBase<string>
{
	public virtual string Name { get; set; }

	public string ParentId { get; set; }
}

