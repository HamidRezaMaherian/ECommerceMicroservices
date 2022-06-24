namespace UI.Application.DTOs;
public abstract class AboutUsDTO
{
	public string Title { get; set; }
	public string ShortDesc { get; set; }
	public string Description { get; set; }
	public string ImagePath { get; set; }
	public bool IsActive { get; set; }
}
