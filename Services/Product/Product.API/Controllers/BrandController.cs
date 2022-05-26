
using Microsoft.AspNetCore.Mvc;
using Product.API.Configurations.DTOs;
using Product.Application.Services;
using Product.Domain.Entities;
using Services.Shared.Resources;

namespace Product.API.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class BrandController : ControllerBase
	{
		private readonly IBrandService _brandService;

		public BrandController(IBrandService brandService)
		{
			_brandService = brandService;
		}
		[HttpGet]
		public ActionResult<IEnumerable<Brand>> GetAll()
		{
			return _brandService.GetAll().ToList();
		}
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult Create([FromBody] BrandDTO brandDTO)
		{
			_brandService.Add(brandDTO);
			return Created("",brandDTO);
		}
		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult Update([FromBody] BrandDTO brandDTO)
		{
			_brandService.Update(brandDTO);
			return Ok();
		}
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult Delete(string id)
		{
			if (!_brandService.Exists(i => i.Id == id))
				return NotFound(
					string.Format(Messages.NOT_FOUND, nameof(Domain.Entities.Product)));

			_brandService.Delete(id);
			return Ok();
		}
	}
}