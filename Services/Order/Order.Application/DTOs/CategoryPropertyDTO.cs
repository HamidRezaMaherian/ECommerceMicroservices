namespace Order.Application.DTOs;
public class CategoryPropertyDTO
{
	public string Id { get; set; }
	public virtual string PropertyId { get; set; }
	public virtual string CategoryId { get; set; }
}
