namespace Product.Application.DTOs;

public abstract class BrandDTO
{
	public virtual string Id { get; set; }
	public virtual string Name { get; set; }
	public virtual string ImagePath { get; set; }

	public abstract object GetImage();
}