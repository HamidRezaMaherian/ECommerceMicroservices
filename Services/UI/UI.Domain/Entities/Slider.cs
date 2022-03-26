using UI.Domain.Common;

namespace UI.Domain.Entities;
public class Slider : EntityBase<string>
{
	public string Title { get; set; }
	public string ImagePath { get; set; }
}
