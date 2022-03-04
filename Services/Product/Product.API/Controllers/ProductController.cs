
using Microsoft.AspNetCore.Mvc;
using Product.Application.DTOs;
using Product.Application.Services;

namespace Product.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
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
		public IActionResult Create([FromBody] ProductDTO productDTO)
		{
			_productService.Add(productDTO);
			return Ok();
		}
		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public IActionResult Update([FromBody] ProductDTO productDTO)
		{
			_productService.Update(productDTO);
			return Ok();
		}
		[HttpDelete]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public IActionResult Delete(object id)
		{
			_productService.Delete(id);
			return Ok();
		}
	}
}