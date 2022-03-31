using Inventory.Domain.Common;

namespace Inventory.Application.DTOs
{
	public class StockDTO : EntityBase<string>
	{
		public string ProductId { get; set; }
		public string PropertyId { get; set; }
		public string StoreId { get; set; }
		public int Count { get; set; }
	}
}
