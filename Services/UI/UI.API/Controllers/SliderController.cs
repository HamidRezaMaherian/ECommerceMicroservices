using Microsoft.AspNetCore.Mvc;
using Services.Shared.Resources;
using UI.Application.DTOs;
using UI.Application.Services;
using UI.Domain.Entities;

namespace UI.API.Controllers
{
	[Route("[controller]/[action]")]
	[ApiController]
	public class SliderController : ControllerBase
	{
		private readonly ISliderService _sliderService;

		public SliderController(ISliderService sliderService)
		{
			_sliderService = sliderService;
		}

		[HttpGet]
		public ActionResult<IEnumerable<Slider>> GetAll()
		{
			return _sliderService.GetAll().ToList();
		}
		[HttpPost]
		public IActionResult Create([FromBody] SliderDTO slider)
		{
			_sliderService.Add(slider);
			return Created("",slider);
		}
		[HttpPut]
		public IActionResult Update([FromBody] SliderDTO slider)
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
