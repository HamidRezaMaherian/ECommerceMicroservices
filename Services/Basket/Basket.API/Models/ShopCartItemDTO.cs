using Basket.Domain.Entities;

namespace Basket.API.Models
{
	public class ShopCartItemDTO
	{
		public string CartId { get; set; }
		public ShopCartItem Item { get; set; }
	}
}