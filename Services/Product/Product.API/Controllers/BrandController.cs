
using Microsoft.AspNetCore.Mvc;
using Product.Application.DTOs;
using Product.Application.Services;
using Product.Domain.Entities;

namespace Product.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class BrandController : ControllerBase
	{
		private readonly IBrandService _productService;

		public BrandController(IBrandService productService)
		{
			_productService = productService;
		}
		[HttpGet]
		public ActionResult<IEnumerable<Brand>> GetAll()
		{
			return _productService.GetAll().ToList();
		}
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult Create([FromBody] BrandDTO productDTO)
		{
			try
			{
				_productService.Add(productDTO);
			}
			catch (Exception e)
			{
				return BadRequest(e);
			}
			return Ok();
		}
		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult Update([FromBody] BrandDTO productDTO)
		{
			try
			{
				_productService.Update(productDTO);
			}
			catch (Exception e)
			{
				return BadRequest(e);
			}
			return Ok();
		}
		[HttpDelete]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult Delete(object id)
		{
			try
			{
				_productService.Delete(id);
			}
			catch (Exception e)
			{
				return BadRequest(e);
			}
			return Ok();
		}
	}
}