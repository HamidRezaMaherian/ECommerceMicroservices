namespace UI.Application.DTOs;
public class FaqCategoryDTO
{
	public string Id { get; set; }
	public string Name { get; set; }
	public bool IsActive { get; set; }
	#region NavigationProps
	public IReadOnlyCollection<FaqDTO> FAQs { get; set; }
	#endregion
}
