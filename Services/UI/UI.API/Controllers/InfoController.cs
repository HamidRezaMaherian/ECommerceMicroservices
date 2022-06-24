using Microsoft.AspNetCore.Mvc;
using UI.API.Configurations.DTOs;
using UI.Application.Services;
using UI.Domain.Entities;

namespace UI.API.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class InfoController : ControllerBase
	{
		private readonly IAboutUsService _aboutUsService;
		private readonly IContactUsService _contactUsService;

		public InfoController(IAboutUsService aboutUsService, IContactUsService contactUsService)
		{
			_aboutUsService = aboutUsService;
			_contactUsService = contactUsService;
		}
		[HttpGet("aboutus")]
		public ActionResult<AboutUs> GetAboutUs()
		{
			return _aboutUsService.FirstOrDefault();
		}
		[HttpGet("contactus")]
		public ActionResult<ContactUs> GetContactUs()
		{
			return _contactUsService.FirstOrDefault();
		}
		[HttpPut("aboutus")]
		public IActionResult UpdateAboutUs(UpdateAboutUsDTO aboutUs)
		{
			_aboutUsService.Update(aboutUs);
			return Ok();
		}
		[HttpPut("contactus")]
		public IActionResult UpdateContactUs(UpdateContactUsDTO contactUs)
		{
			_contactUsService.Update(contactUs);
			return Ok();
		}
	}
}
