using MongoDB.Bson.Serialization.Attributes;
using UI.Infrastructure.Persist.DAOs;

namespace UI.Infrastructure.Persist.DAOs;
public class ContactUsDAO : EntityPrimaryBaseDAO<string>
{
	[BsonRequired]
	public string Address { get; set; }
	[BsonRequired]
	public string Email { get; set; }
	[BsonRequired]
	public string PhoneNumber { get; set; }
	[BsonRequired]
	public string Location { get; set; }
	public string Lat { get; set; }
	public string Lng { get; set; }
}
