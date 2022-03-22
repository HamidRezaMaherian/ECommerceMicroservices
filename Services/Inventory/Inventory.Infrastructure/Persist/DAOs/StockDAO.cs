using MongoDB.Bson.Serialization.Attributes;

namespace Inventory.Infrastructure.Persist.DAOs
{
	[BsonIgnoreExtraElements]
	public class StockDAO : EntityBaseDAO<string>
	{
		[BsonRequired]
		public string ProductId { get; set; }
		[BsonRequired]
		public string StoreId { get; set; }
		[BsonRequired]
		public int Count { get; set; }
		#region NavigationProps
		public StoreDAO Store { get; set; }
		#endregion
	}
}
