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
	public class SocialMediaController : ControllerBase
	{
		private readonly ISocialMediaService _socialMediaService;

		public SocialMediaController(ISocialMediaService socialMediaService)
		{
			_socialMediaService = socialMediaService;
		}
		[HttpGet]
		public ActionResult<IEnumerable<SocialMedia>> GetAll()
		{
			return _socialMediaService.GetAll().ToList();
		}
		[HttpPost]
		public IActionResult Create([FromBody]CreateSocialMediaDTO socialMedia)
		{
			_socialMediaService.Add(socialMedia);
			return Created("",socialMedia);
		}
		[HttpPut]
		public IActionResult Update([FromBody] UpdateSocialMediaDTO socialMedia)
		{
			_socialMediaService.Update(socialMedia);
			return Ok(string.Format(Messages.SUCCEDED, "UPDATE"));
		}
		[HttpDelete("{id}")]
		public IActionResult Delete(string id)
		{
			if (!_socialMediaService.Exists(i => i.Id == id))
				return NotFound(
					string.Format(Messages.NOT_FOUND, nameof(FAQ)));

			_socialMediaService.Delete(id);
			return Ok(string.Format(Messages.SUCCEDED, "Deletion"));
		}
	}
}
