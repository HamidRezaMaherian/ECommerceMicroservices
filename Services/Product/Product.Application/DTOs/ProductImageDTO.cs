using Product.Domain.Common;

namespace Product.Application.DTOs;

public class ProductImageDTO : EntityBase<string>
{
	public virtual string ImagePath { get; set; }
	public virtual string ProductId { get; set; }
}
