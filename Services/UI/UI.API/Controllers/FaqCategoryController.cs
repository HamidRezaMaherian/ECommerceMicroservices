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

		public FaqCategoryController(IFaqCategoryService sliderService)
		{
			_faqCategoryService = sliderService;
		}

		[HttpGet]
		public ActionResult<IEnumerable<FaqCategory>> GetAll()
		{
			return _faqCategoryService.GetAll().ToList();
		}
		[HttpPost]
		public IActionResult Create([FromBody] FaqCategoryDTO slider)
		{
			_faqCategoryService.Add(slider);
			return Ok(string.Format(Messages.SUCCEDED, "CREATION"));
		}
		[HttpPut]
		public IActionResult Update([FromBody] FaqCategoryDTO slider)
		{
			_faqCategoryService.Update(slider);
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
