using Inventory.Application.DTOs;
using Inventory.Application.Services;
using Inventory.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class StockController : ControllerBase
	{
		private readonly IStockService _stockService;

		public StockController(IStockService stockService)
		{
			_stockService = stockService;
		}

		[HttpGet("{productId}")]
		public ActionResult<IEnumerable<Stock>> GetAllForProduct(string productId)
		{
			return _stockService.GetAll(i=>i.ProductId==productId).ToList();
		}
		[HttpGet("{storeId}")]
		public ActionResult<IEnumerable<Stock>> GetAllForStore(string storeId)
		{
			return _stockService.GetAll(i => i.StoreId == storeId).ToList();
		}
		[HttpPost]
		public IActionResult Create([FromBody]StockDTO stockDTO)
		{
			_stockService.Add(stockDTO);
			return Ok();
		}
		[HttpPut]
		public IActionResult Update([FromBody] StockDTO stockDTO)
		{
			_stockService.Update(stockDTO);
			return Ok();
		}
		[HttpDelete("{stockId}")]
		public IActionResult Delete(string stockId)
		{
			_stockService.Delete(stockId);
			return Ok();
		}
	}
}