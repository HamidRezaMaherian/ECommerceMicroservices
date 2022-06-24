namespace UI.Application.DTOs;
public abstract class FaqDTO:BaseDTO<string>
{
	public virtual string Question { get; set; }
	public virtual string Answer { get; set; }
	public virtual string CategoryId { get; set; }
	public virtual bool IsActive { get; set; }
}
