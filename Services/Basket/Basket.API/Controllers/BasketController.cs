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

		[HttpGet("Get")]
		public ActionResult<ShopCart> GetBasket(string userName)
		{
			return _shopCartRepo.Get(userName);
		}
		[HttpPost("Create")]
		public IActionResult CreateBasket([FromBody] ShopCartDTO shopCart)
		{
			var entity = new ShopCart() { UserName = shopCart.UserName };
			_shopCartRepo.Add(entity);
			return Created("", entity);
		}
		[HttpPost("/AddItem")]
		public IActionResult AddBasketItem([FromBody] ShopCartItemDTO shopCartItem)
		{
			_shopCartRepo.AddItem(shopCartItem.CartId, shopCartItem.Item);
			return Created("", shopCartItem);
		}
		[HttpPut("/UpdateItem")]
		public IActionResult UpdateBasketItem([FromBody] ShopCartItemDTO shopCartItem)
		{
			_shopCartRepo.UpdateItem(shopCartItem.CartId, shopCartItem.Item);
			return Ok();
		}
		[HttpDelete("/DeleteItem")]
		public IActionResult DeleteBasketItem([FromBody] ShopCartItemDTO shopCartItem)
		{
			_shopCartRepo.DeleteItem(shopCartItem.CartId, shopCartItem.Item);
			return Ok();
		}
		[HttpDelete("{userName}")]
		public IActionResult DeleteBasket(string userName)
		{
			_shopCartRepo.Delete(userName);
			return Ok();
		}
	}
}