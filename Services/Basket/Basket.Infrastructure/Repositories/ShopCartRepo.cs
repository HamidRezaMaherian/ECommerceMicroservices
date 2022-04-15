using Basket.Application.Exceptions;
using Basket.Application.Repositories;
using Basket.Domain.Common;
using Basket.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Services.Shared.AppUtils;

namespace Basket.Infrastructure.Repositories
{
	public class ShopCartRepo : Repository<ShopCart>, IShopCartRepo
	{
		public ShopCartRepo(IDistributedCache cache) : base(cache)
		{
		}

		public void AddItem(string cartId, ShopCartItem item)
		{
			throw new NotImplementedException();
		}

		public void DeleteItem(string cartId, ShopCartItem item)
		{
			throw new NotImplementedException();
		}

		public void UpdateItem(string cartId, ShopCartItem item)
		{
			throw new NotImplementedException();
		}
	}
}
