using MongoDB.Bson.Serialization.Attributes;
using UI.Infrastructure.Persist.DAOs;

namespace UI.Infrastructure.Persist.DAOs;
public class SliderDAO : EntityBaseDAO<string>
{
	[BsonRequired]
	public string Title { get; set; }
	[BsonRequired]
	public string ImagePath { get; set; }
}
