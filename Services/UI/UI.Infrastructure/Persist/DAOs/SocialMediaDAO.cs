using MongoDB.Bson.Serialization.Attributes;
using UI.Infrastructure.Persist.DAOs;

namespace UI.Infrastructure.Persist.DAOs;
public class SocialMediaDAO : EntityBaseDAO<string>
{
	[BsonRequired]
	public string Name { get; set; }
	[BsonRequired]
	public string Link { get; set; }
	[BsonRequired]
	public string ImagePath { get; set; }
}