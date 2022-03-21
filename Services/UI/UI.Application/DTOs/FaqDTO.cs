namespace UI.Application.DTOs;
public class FaqDTO
{
	public string Id { get; set; }
	public string Question { get; set; }
	public string Answer { get; set; }
	public string CategoryId { get; set; }
	public bool IsActive { get; set; }
	#region NavigationProps
	public FaqCategoryDTO Category { get; set; }
	#endregion
}
