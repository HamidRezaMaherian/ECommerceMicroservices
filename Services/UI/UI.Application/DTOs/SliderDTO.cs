namespace UI.Application.DTOs;
public abstract class SliderDTO:BaseDTO<string>
{
	public virtual string Title { get; set; }
	public virtual string ImagePath { get; set; }
	public virtual bool IsActive { get; set; }
	public abstract object GetImage();
}
