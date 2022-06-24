using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UI.API.Configurations.DTOs;
using UI.API.Controllers;
using UI.API.Tests.Utils;
using UI.Application.DTOs;
using UI.Application.Services;
using UI.Application.Tools;
using UI.Domain.Entities;
using static UI.API.Tests.Utils.TestUtilsExtension;

namespace UI.API.Tests.Unit
{
	[TestFixture]
	public class SocialMediaControllerTests
	{
		private ISocialMediaService _socialMediaService;
		private ICustomMapper _customMapper;
		private SocialMediaController _sliderController;
		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			var _sliders = new List<SocialMedia>();
			_customMapper = CreateMapper(new TestMapperProfile());
			_socialMediaService = MockAction<SocialMedia, SocialMediaDTO>
				.MockServie<ISocialMediaService>(_sliders).Object;
			_sliderController = new SocialMediaController(_socialMediaService);
		}
		[Test]
		public void GetAll_ReturnAllUIs()
		{
			var slider = CreateSocialMedia();
			var res = _sliderController.GetAll();
			CollectionAssert.AreEquivalent(res.Value?.Select(i => i.Id), _socialMediaService.GetAll().Select(i => i.Id));
		}
		[Test]
		public void Create_PasValidEntity_AddSocialMedia()
		{
			var slider = new CreateSocialMediaDTO()
			{
				ImagePath = "no image",
				IsActive = true,
				Name = "no title",
				Link = "#"
			};
			_sliderController.Create(slider);
			Assert.IsNotNull(_socialMediaService.GetById(slider.Id));
		}
		[Test]
		public void Update_PasValidEntity_UpdateSocialMedia()
		{
			var slider = _customMapper.Map<UpdateSocialMediaDTO>(CreateSocialMedia());
			slider.Name = "updatedTest";
			_sliderController.Update(slider);

			Assert.AreEqual(_socialMediaService.GetById(slider.Id).Name, slider.Name);
		}
		[Test]
		public void Delete_PasValidId_DeleteSocialMedia()
		{
			var slider = CreateSocialMedia();
			var result = _sliderController.Delete(slider.Id);

			Assert.IsFalse(_socialMediaService.Exists(i => i.Id == slider.Id));
		}
		#region HelperMethods
		private SocialMedia CreateSocialMedia()
		{
			var slider = new CreateSocialMediaDTO()
			{
				Name = "test",
				Link = "#",
				ImagePath = "no image",
				IsActive = true
			};
			_socialMediaService.Add(slider);
			return _socialMediaService.GetById(slider.Id);
		}
		#endregion
	}
}
