using Product.Domain.Common;

namespace Product.Application.DTOs;
public class ProductPropertyDTO : EntityBase<string>
{
	public virtual string Value { get; set; }
	public virtual string PropertyId { get; set; }
	public virtual string ProductId { get; set; }
}
