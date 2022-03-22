using MongoDB.Bson.Serialization.Attributes;

namespace Inventory.Infrastructure.Persist.DAOs
{
	[BsonIgnoreExtraElements]
	public class StoreDAO : EntityBaseDAO<string>
	{
		[BsonRequired]
		public string Name { get; set; }
		[BsonRequired]
		public string ShortDesc { get; set; }
		[BsonRequired]
		public string Description { get; set; }

		#region NavigationProps
		public virtual IEnumerable<StockDAO> Stocks { get; set; }
		#endregion
	}
}
