using Order.Domain.Common;

namespace Order.Application.DTOs;
public class ProductPropertyDTO : EntityBase<string>
{
	public virtual string Value { get; set; }
	public virtual string PropertyId { get; set; }
	public virtual string ProductId { get; set; }
}
