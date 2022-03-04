using Microsoft.AspNetCore.Mvc;
using Product.Application.DTOs;
using Product.Application.Services;

namespace Product.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductCategoryController : ControllerBase
{
	private readonly IProductCategoryService _productCategoryService;

	public ProductCategoryController(IProductCategoryService productService)
	{
		_productCategoryService = productService;
	}
	[HttpGet]
	public ActionResult<IEnumerable<Domain.Entities.ProductCategory>> GetAll()
	{
		return _productCategoryService.GetAll().ToList();
	}
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public IActionResult Create([FromBody] ProductCategoryDTO productCategoryDTO)
	{
		_productCategoryService.Add(productCategoryDTO);
		return Ok();
	}
	[HttpPut]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public IActionResult Update([FromBody] ProductCategoryDTO productCategoryDTO)
	{
		_productCategoryService.Update(productCategoryDTO);
		return Ok();
	}
	[HttpDelete]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public IActionResult Delete(object id)
	{
		_productCategoryService.Delete(id);
		return Ok();
	}
}