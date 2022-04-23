using Microsoft.AspNetCore.Mvc;
using Services.Shared.Resources;
using UI.Application.DTOs;
using UI.Application.Services;
using UI.Domain.Entities;

namespace UI.API.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class FaqCategoryController : ControllerBase
	{
		private readonly IFaqCategoryService _faqCategoryService;

		public FaqCategoryController(IFaqCategoryService faqCategoryService)
		{
			_faqCategoryService = faqCategoryService;
		}

		[HttpGet]
		public ActionResult<IEnumerable<FaqCategory>> GetAll()
		{
			return _faqCategoryService.GetAll().ToList();
		}
		[HttpPost]
		public IActionResult Create([FromBody] FaqCategoryDTO faqCategory)
		{
			_faqCategoryService.Add(faqCategory);
			return Created("",faqCategory);
		}
		[HttpPut]
		public IActionResult Update([FromBody] FaqCategoryDTO faqCategory)
		{
			_faqCategoryService.Update(faqCategory);
			return Ok(string.Format(Messages.SUCCEDED, "UPDATE"));
		}
		[HttpDelete("{id}")]
		public IActionResult Delete(string id)
		{
			if (!_faqCategoryService.Exists(i => i.Id == id))
				return NotFound(
					string.Format(Messages.NOT_FOUND, nameof(FaqCategory)));

			_faqCategoryService.Delete(id);
			return Ok(string.Format(Messages.SUCCEDED, "Deletion"));
		}
	}
}
