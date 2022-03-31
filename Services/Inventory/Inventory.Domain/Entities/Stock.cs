using Inventory.Domain.Common;

namespace Inventory.Domain.Entities
{
	public class Stock : EntityBase<string>
	{
		public string ProductId { get; set; }
		public string PropertyId { get; set; }
		public string StoreId { get; set; }
		public int Count { get; set; }
		#region NavigationProps
		public Store Store { get; set; }
		#endregion
	}
}
