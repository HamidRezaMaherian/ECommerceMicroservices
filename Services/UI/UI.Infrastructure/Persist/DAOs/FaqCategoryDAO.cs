using MongoDB.Bson.Serialization.Attributes;
using UI.Infrastructure.Persist.DAOs;

namespace UI.Infrastructure.Persist.DAOs;
public class FaqCategoryDAO : EntityBaseDAO<string>
{
	[BsonRequired]
	public string Name { get; set; }
	#region NavigationProps
	public IReadOnlyCollection<FaqDAO> FAQs { get; set; }
	#endregion
}
