using Inventory.API.Configurations.DTOs;
using Inventory.Application.Services;
using Inventory.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Shared.Resources;

namespace Inventory.API.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
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
			return Created("",storeDTO);
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
			if (!_storeService.Exists(i => i.Id == storeId))
				return NotFound(
					string.Format(Messages.NOT_FOUND, nameof(Stock)));
			_storeService.Delete(storeId);
			return Ok();
		}

	}
}