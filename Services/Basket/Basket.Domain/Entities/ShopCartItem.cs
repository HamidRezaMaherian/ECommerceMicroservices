using Basket.Domain.Common;

namespace Basket.Domain.Entities
{
	public class ShopCartItem:EntityPrimaryBase<string>
	{
		public string ProductId { get; set; }
		public string PropertyId { get; set; }
		public uint Count { get; set; }
		public uint Price { get; set; }
	}
}
