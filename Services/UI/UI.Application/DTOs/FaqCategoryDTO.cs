namespace UI.Application.DTOs;
public abstract class FaqCategoryDTO:BaseDTO<string>
{
	public virtual string Name { get; set; }
	public virtual bool IsActive { get; set; }
}
