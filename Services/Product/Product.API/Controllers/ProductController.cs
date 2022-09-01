
using Microsoft.AspNetCore.Mvc;
using Product.API.Configurations.DTOs;
using Product.Application.Services;
using Services.Shared.Resources;

namespace Product.API.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}
		[HttpGet]
		public ActionResult<IEnumerable<Domain.Entities.Product>> GetAll()
		{
			return _productService.GetAll().ToList();
		}
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public IActionResult Create([FromBody] CreateProductDTO productDTO)
		{
			_productService.Add(productDTO);
			return Created("", productDTO);
		}
		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public IActionResult Update([FromBody] UpdateProductDTO productDTO)
		{
			_productService.Update(productDTO);
			return Ok();
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult Delete(string id)
		{
			if (!_productService.Exists(i => i.Id == id))
				return NotFound(
					string.Format(Messages.NOT_FOUND, nameof(Domain.Entities.Product)));
			_productService.Delete(id);
			return Ok();
		}
	}
}