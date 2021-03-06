namespace Product.Application.DTOs;

public class ProductDTO
{
	public string Id { get; set; }
	public virtual string Name { get; set; }
	public virtual string ShortDesc { get; set; }
	public virtual string Description { get; set; }

	public virtual decimal UnitPrice { get; set; }

	public virtual string MainImagePath { get; set; }

	public virtual DateTime CreatedDateTime { get; set; }

	public string CategoryId { get; set; }
}
