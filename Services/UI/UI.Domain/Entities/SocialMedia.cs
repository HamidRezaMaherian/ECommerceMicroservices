using Services.Shared.Common;

namespace UI.Domain.Entities;
public class SocialMedia : EntityBase<string>
{
	public string Name { get; set; }
	public string Link { get; set; }
	public string ImagePath { get; set; }
}