using Basket.Domain.Entities;

namespace Basket.Application.Repositories
{
	public interface IShopCartRepo : IRepository<ShopCart>
	{
		void AddItem(string cartId, ShopCartItem item);
		void DeleteItem(string cartId, ShopCartItem item);
		void UpdateItem(string cartId, ShopCartItem item);
	}
}
