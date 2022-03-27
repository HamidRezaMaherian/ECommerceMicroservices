using Order.Domain.Common;

namespace Order.Application.DTOs;

public class ProductImageDTO : EntityBase<string>
{
	public virtual string ImagePath { get; set; }
	public virtual string ProductId { get; set; }
}
