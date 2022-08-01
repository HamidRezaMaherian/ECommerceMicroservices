using UI.Domain.Common;
using UI.Domain.ValueObjects;

namespace UI.Domain.Entities;
public class SocialMedia : EntityBase<string>
{
	public string Name { get; set; }
	public string Link { get; set; }
	public Blob Image { get; set; }
}