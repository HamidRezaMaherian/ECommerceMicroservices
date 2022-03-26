using UI.Domain.Common;

namespace UI.Domain.Entities;
public class FaqCategory : EntityBase<string>
{
	public string Name { get; set; }
	#region NavigationProps
	public IReadOnlyCollection<FAQ> FAQs { get; set; }
	#endregion
}
