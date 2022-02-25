using Product.Domain.Common;

namespace Product.Application.DTOs;

public class ProductImageDTO : EntityBase<string>
{
	public virtual string ImagePath { get; set; }
	#region Relations
	public virtual string ProductId { get; set; }
	public Product Product { get; set; }
	#endregion
}
