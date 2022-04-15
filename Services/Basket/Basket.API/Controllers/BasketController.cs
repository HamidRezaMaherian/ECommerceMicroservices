using Basket.API.Models;
using Basket.Application.Repositories;
using Basket.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class BasketController : ControllerBase
	{
		private readonly IShopCartRepo _shopCartRepo;

		public BasketController(IShopCartRepo shopCartRepo)
		{
			_shopCartRepo = shopCartRepo;
		}

		[HttpGet("Basket")]
		public ActionResult<ShopCart> GetBasket(string userName)
		{
			return _shopCartRepo.Get(userName);
		}
		[HttpPost("Basket")]
		public IActionResult CreateBasket([FromBody] ShopCart shopCart)
		{
			_shopCartRepo.Add(shopCart);
			return Ok();
		}
		[HttpPost("Basket/Item")]
		public IActionResult AddBasketItem([FromBody] ShopCartItemDTO shopCartItem)
		{
			_shopCartRepo.AddItem(shopCartItem.CartId, shopCartItem.Item);
			return Ok();
		}
		[HttpPut("Basket/Item")]
		public IActionResult UpdateBasketItem([FromBody] ShopCartItemDTO shopCartItem)
		{
			_shopCartRepo.UpdateItem(shopCartItem.CartId, shopCartItem.Item);
			return Ok();
		}
		[HttpDelete("Basket/Item")]
		public IActionResult DeleteBasketItem([FromBody] ShopCartItemDTO shopCartItem)
		{
			_shopCartRepo.DeleteItem(shopCartItem.CartId, shopCartItem.Item);
			return Ok();
		}
		[HttpDelete("Basket/{userName}")]
		public IActionResult DeleteBasket(string userName)
		{
			_shopCartRepo.Delete(userName);
			return Ok();
		}
	}
}