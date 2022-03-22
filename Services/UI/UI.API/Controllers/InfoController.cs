using Microsoft.AspNetCore.Mvc;
using Services.Shared.Resources;
using UI.Application.DTOs;
using UI.Application.Services;
using UI.Domain.Entities;

namespace UI.API.Controllers
{
	[Route("api/[controller]")]
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
		public ActionResult<AboutUs> GetAboutUs()
		{
			return _aboutUsService.FirstOrDefault();
		}
		public ActionResult<ContactUs> GetContactUs()
		{
			return _contactUsService.FirstOrDefault();
		}
		public IActionResult UpdateAboutUs(AboutUsDTO aboutUs)
		{
			throw new NotImplementedException();
		}
		public IActionResult UpdateContactUs(ContactUsDTO contactUs)
		{
			throw new NotImplementedException();
		}
	}
}
