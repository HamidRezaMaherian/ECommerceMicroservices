
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
		public IActionResult Create(ProductDTO productDTO)
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
	}
}