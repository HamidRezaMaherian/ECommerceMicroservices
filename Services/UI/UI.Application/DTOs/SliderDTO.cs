namespace UI.Application.DTOs;
public abstract class SliderDTO
{
	public virtual string Id { get; set; }
	public virtual string Title { get; set; }
	public virtual string ImagePath { get; set; }
	public virtual bool IsActive { get; set; }
}
