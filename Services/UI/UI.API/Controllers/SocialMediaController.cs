﻿using Microsoft.AspNetCore.Mvc;
using Services.Shared.Resources;
using UI.Application.DTOs;
using UI.Application.Services;
using UI.Domain.Entities;

namespace UI.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SocialMediaController : ControllerBase
	{
		private readonly ISocialMediaService _sliderService;

		public SocialMediaController(ISocialMediaService sliderService)
		{
			_sliderService = sliderService;
		}

		public ActionResult<IEnumerable<SocialMedia>> GetAll()
		{
			return _sliderService.GetAll().ToList();
		}
		public IActionResult Create(SocialMediaDTO slider)
		{
			_sliderService.Add(slider);
			return Ok(string.Format(Messages.SUCCEDED, "CREATION"));
		}
		public IActionResult Update(SocialMediaDTO slider)
		{
			_sliderService.Update(slider);
			return Ok(string.Format(Messages.SUCCEDED, "UPDATE"));
		}
		public IActionResult Delete(string id)
		{
			_sliderService.Delete(id);
			return Ok(string.Format(Messages.SUCCEDED, "Deletion"));
		}
	}
}
