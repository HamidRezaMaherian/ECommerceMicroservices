using Inventory.Domain.Common;

namespace Inventory.Domain.Entities
{
	public class Store : EntityBase<string>
	{
		public string Name { get; set; }
		public string ShortDesc { get; set; }
		public string Description { get; set; }
		public string LogoPath { get; set; }
		#region NavigationProps
		public virtual IEnumerable<Stock> Stocks { get; set; }
		#endregion
	}
}
