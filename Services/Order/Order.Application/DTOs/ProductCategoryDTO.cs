using Order.Domain.Common;

namespace Order.Application.DTOs;
public class ProductCategoryDTO : EntityBase<string>
{
	public virtual string Name { get; set; }

	public string ParentId { get; set; }
}

