using Inventory.Application.DTOs;
using Inventory.Application.Services;
using Inventory.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class StoreController : ControllerBase
	{
		private readonly IStoreService _storeService;

		public StoreController(IStoreService storeService)
		{
			_storeService = storeService;
		}

		[HttpGet]
		public ActionResult<IEnumerable<Store>> GetAll()
		{
			return _storeService.GetAll().ToList();
		}
		[HttpPost]
		public IActionResult Create([FromBody] StoreDTO storeDTO)
		{
			_storeService.Add(storeDTO);
			return Ok();
		}
		[HttpPut]
		public IActionResult Update([FromBody] StoreDTO storeDTO)
		{
			_storeService.Update(storeDTO);
			return Ok();
		}
		[HttpDelete("{storeId}")]
		public IActionResult Delete(string storeId)
		{
			_storeService.Delete(storeId);
			return Ok();
		}

	}
}