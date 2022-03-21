using Microsoft.AspNetCore.Mvc;
using Services.Shared.Resources;
using UI.Application.DTOs;
using UI.Application.Services;
using UI.Domain.Entities;

namespace UI.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FaqCategoryController : ControllerBase
	{
		private readonly IFaqCategoryService _faqCategoryService;

		public FaqCategoryController(IFaqCategoryService sliderService)
		{
			_faqCategoryService = sliderService;
		}

		public ActionResult<IEnumerable<FaqCategory>> GetAll()
		{
			return _faqCategoryService.GetAll().ToList();
		}
		public IActionResult Create(FaqCategoryDTO slider)
		{
			_faqCategoryService.Add(slider);
			return Ok(string.Format(Messages.SUCCEDED, "CREATION"));
		}
		public IActionResult Update(FaqCategoryDTO slider)
		{
			_faqCategoryService.Update(slider);
			return Ok(string.Format(Messages.SUCCEDED, "UPDATE"));
		}
		public IActionResult Delete(string id)
		{
			_faqCategoryService.Delete(id);
			return Ok(string.Format(Messages.SUCCEDED, "Deletion"));
		}
	}
}
