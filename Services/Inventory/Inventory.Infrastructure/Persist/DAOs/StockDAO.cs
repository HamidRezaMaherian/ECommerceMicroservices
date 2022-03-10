namespace Inventory.Infrastructure.Persist.DAOs
{
	public class StockDAO : EntityBaseDAO<string>
	{
		public string ProductId { get; set; }
		public string StoreId { get; set; }
		public int Count { get; set; }
		#region NavigationProps
		public StoreDAO Store { get; set; }
		#endregion
	}
}
