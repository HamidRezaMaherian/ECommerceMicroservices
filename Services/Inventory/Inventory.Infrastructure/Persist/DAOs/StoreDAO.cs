namespace Inventory.Infrastructure.Persist.DAOs
{
	public class StoreDAO : EntityBaseDAO<string>
	{
		public string Name { get; set; }
		public string ShortDesc { get; set; }
		public string Description { get; set; }

		#region NavigationProps
		public virtual IEnumerable<StockDAO> Stocks { get; set; }
		#endregion
	}
}
