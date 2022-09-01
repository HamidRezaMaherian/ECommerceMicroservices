namespace Product.Application.DTOs;

public abstract class ProductDTO
{
	public virtual string Id { get; set; }
	public virtual string Name { get; set; }
	public virtual string ShortDesc { get; set; }
	public virtual string Description { get; set; }

	public virtual decimal UnitPrice { get; set; }

	public virtual string MainImagePath { get; set; }

	public abstract object GetMainImage();
	public virtual DateTime CreatedDateTime { get; set; }

	public string CategoryId { get; set; }
}
