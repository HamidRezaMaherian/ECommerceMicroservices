using MongoDB.Bson.Serialization.Attributes;

namespace UI.Infrastructure.Persist.DAOs;
public class FaqDAO : EntityBaseDAO<string>
{
	[BsonRequired]
	public string Question { get; set; }
	[BsonRequired]
	public string Answer { get; set; }
	public string CategoryId { get; set; }
	#region NavigationProps
	public FaqCategoryDAO Category { get; set; }
	#endregion
}
