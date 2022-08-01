using UI.Domain.Common;
using UI.Domain.ValueObjects;

namespace UI.Domain.Entities;
public class Slider : EntityBase<string>
{
	public string Title { get; set; }
	public Blob Image { get; set; }
}
