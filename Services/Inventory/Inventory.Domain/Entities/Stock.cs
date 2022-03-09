using Services.Shared.Common;

namespace Inventory.Domain.Entities
{
	public class Stock : EntityBase<string>
	{
		public string ProductId { get; set; }
		public int Count { get; set; }
		#region NavigationProps
		public Store Store { get; set; }
		#endregion
	}
}
