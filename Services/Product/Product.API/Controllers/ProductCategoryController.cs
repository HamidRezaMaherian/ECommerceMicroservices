using Microsoft.AspNetCore.Mvc;
using Product.Application.DTOs;
using Product.Application.Services;
using Services.Shared.Resources;

namespace Product.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
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
	[HttpDelete("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public IActionResult Delete(string id)
	{
		if (!_productCategoryService.Exists(i => i.Id == id))
			return NotFound(
				string.Format(Messages.NOT_FOUND, nameof(Domain.Entities.Product)));

		_productCategoryService.Delete(id);
		return Ok();
	}
}