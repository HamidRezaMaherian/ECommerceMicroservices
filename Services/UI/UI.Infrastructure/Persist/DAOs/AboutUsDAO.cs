using MongoDB.Bson.Serialization.Attributes;
using UI.Infrastructure.Persist.DAOs;

namespace UI.Infrastructure.Persist.DAOs;
public class AboutUsDAO : EntityPrimaryBaseDAO<string>
{
	[BsonRequired]
	public string Title { get; set; }
	[BsonRequired]
	public string ShortDesc { get; set; }
	[BsonRequired]
	public string Description { get; set; }
	public string ImagePath { get; set; }
}
