namespace Order.Application.DTOs;

public class BrandDTO
{
	public string Id { get; set; }
	public virtual string Name { get; set; }
	public virtual string ImagePath { get; set; }
}