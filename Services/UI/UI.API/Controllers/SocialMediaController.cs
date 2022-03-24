using Microsoft.AspNetCore.Mvc;
using Services.Shared.Resources;
using UI.Application.DTOs;
using UI.Application.Services;
using UI.Domain.Entities;

namespace UI.API.Controllers
{
	[Route("[controller]/[action]")]
	[ApiController]
	public class SocialMediaController : ControllerBase
	{
		private readonly ISocialMediaService _sliderService;

		public SocialMediaController(ISocialMediaService sliderService)
		{
			_sliderService = sliderService;
		}
		[HttpGet]
		public ActionResult<IEnumerable<SocialMedia>> GetAll()
		{
			return _sliderService.GetAll().ToList();
		}
		[HttpPost]
		public IActionResult Create([FromBody]SocialMediaDTO slider)
		{
			_sliderService.Add(slider);
			return Ok(string.Format(Messages.SUCCEDED, "CREATION"));
		}
		[HttpPut]
		public IActionResult Update([FromBody] SocialMediaDTO slider)
		{
			_sliderService.Update(slider);
			return Ok(string.Format(Messages.SUCCEDED, "UPDATE"));
		}
		[HttpDelete("{id}")]
		public IActionResult Delete(string id)
		{
			if (!_sliderService.Exists(i => i.Id == id))
				return NotFound(
					string.Format(Messages.NOT_FOUND, nameof(FAQ)));

			_sliderService.Delete(id);
			return Ok(string.Format(Messages.SUCCEDED, "Deletion"));
		}
	}
}
