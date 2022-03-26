using UI.Domain.Common;

namespace UI.Domain.Entities;
public class AboutUs : EntityPrimaryBase<string>
{
	public string Title { get; set; }
	public string ShortDesc { get; set; }
	public string Description { get; set; }
	public string ImagePath { get; set; }
}
