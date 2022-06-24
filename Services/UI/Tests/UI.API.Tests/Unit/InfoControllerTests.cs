using NUnit.Framework;
using System;
using System.Collections.Generic;
using UI.API.Configurations.DTOs;
using UI.API.Controllers;
using UI.API.Tests.Utils;
using UI.Application.Services;
using UI.Domain.Entities;

namespace UI.API.Tests.Unit
{
	[TestFixture]
	public class InfoControllerTests
	{
		private IAboutUsService _aboutUsService;
		private IContactUsService _contactUsService;
		private InfoController _infoController;
		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			var _aboutUs = new List<AboutUs>()
			{
				new AboutUs(){Id=Guid.NewGuid().ToString()}
			};
			var _contactUs = new List<ContactUs>()
			{
				new ContactUs(){Id=Guid.NewGuid().ToString()}
			};
			_aboutUsService = MockActions.MockAboutUsService(_aboutUs);
			_contactUsService = MockActions.MockContactUsService(_contactUs);
			_infoController = new InfoController(_aboutUsService, _contactUsService);
		}
		[Test]
		public void GetContactUs_ReturnContactUs()
		{
			var contactUs = _infoController.GetContactUs();
			Assert.AreEqual(contactUs.Value, _contactUsService.FirstOrDefault());
		}
		[Test]
		public void GetAboutUs_ReturnAboutUs()
		{
			var aboutUs = _infoController.GetAboutUs();
			Assert.AreEqual(aboutUs.Value, _aboutUsService.FirstOrDefault());
		}
		[Test]
		public void UpdateAboutUs_UpdateAboutUs()
		{
			var aboutUs = new UpdateAboutUsDTO()
			{
				ImagePath = "no image",
				Description = "no desc",
				ShortDesc = "no desc",
				IsActive = true,
				Title = "no title",
			};
			_infoController.UpdateAboutUs(aboutUs);
			Assert.AreEqual(aboutUs.Title, _aboutUsService.FirstOrDefault().Title);
		}
		[Test]
		public void UpdateContactUs_UpdateContactUs()
		{
			var contactUs = new UpdateContactUsDTO()
			{
				Email = "no email",
				Address = "no address",
				Lat = "34234",
				Lng = "23234234",
				Location = "no location",
				PhoneNumber = "09304422204"
			};
			_infoController.UpdateContactUs(contactUs);
			Assert.AreEqual(contactUs.Email, _contactUsService.FirstOrDefault().Email);
		}
	}
}
