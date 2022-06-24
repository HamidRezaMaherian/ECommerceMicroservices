using Microsoft.AspNetCore.Mvc;
using Services.Shared.Resources;
using UI.API.Configurations.DTOs;
using UI.Application.DTOs;
using UI.Application.Services;
using UI.Domain.Entities;

namespace UI.API.Controllers
{
	[Route("[controller]/[action]")]
	[ApiController]
	public class FaqController : ControllerBase
	{
		private readonly IFaqService _faqService;

		public FaqController(IFaqService faqService)
		{
			_faqService = faqService;
		}

		[HttpGet]
		public ActionResult<IEnumerable<FAQ>> GetAll()
		{
			return _faqService.GetAll().ToList();
		}
		[HttpPost]
		public IActionResult Create([FromBody] CreateFaqDTO faq)
		{
			_faqService.Add(faq);
			return Created("",faq);
		}
		[HttpPut]
		public IActionResult Update([FromBody] UpdateFaqDTO faq)
		{
			_faqService.Update(faq);
			return Ok(string.Format(Messages.SUCCEDED, "UPDATE"));
		}
		[HttpDelete("{id}")]
		public IActionResult Delete(string id)
		{
			if (!_faqService.Exists(i => i.Id == id))
				return NotFound(
					string.Format(Messages.NOT_FOUND, nameof(FAQ)));
			_faqService.Delete(id);
			return Ok(string.Format(Messages.SUCCEDED, "Deletion"));
		}
	}
}
