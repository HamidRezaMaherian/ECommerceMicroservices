using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UI.API.Controllers;
using UI.Application.DTOs;
using UI.Application.Services;
using UI.Domain.Entities;
using static UI.API.Tests.Utils.TestUtilsExtension;

namespace UI.API.Tests.Unit
{
	[TestFixture]
	public class SocialMediaControllerTests
	{
		private ISocialMediaService _sliderervice;
		private SocialMediaController _sliderController;
		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			var _sliders = new List<SocialMedia>();
			_sliderervice = MockAction<SocialMedia, SocialMediaDTO>
				.MockServie<ISocialMediaService>(_sliders).Object;
			_sliderController = new SocialMediaController(_sliderervice);
		}
		[Test]
		public void GetAll_ReturnAllUIs()
		{
			var slider = CreateSocialMedia();
			var res = _sliderController.GetAll();
			CollectionAssert.AreEquivalent(res.Value?.Select(i => i.Id), _sliderervice.GetAll().Select(i => i.Id));
		}
		[Test]
		public void Create_PasValidEntity_AddSocialMedia()
		{
			var slider = new SocialMediaDTO()
			{
				ImagePath = "no image",
				IsActive = true,
				Name = "no title",
				Link = "#"
			};
			_sliderController.Create(slider);
			Assert.IsNotNull(_sliderervice.GetById(slider.Id));
		}
		[Test]
		public void Update_PasValidEntity_UpdateSocialMedia()
		{
			var slider = CreateSocialMedia();
			slider.Name = "updatedTest";
			_sliderController.Update(slider);

			Assert.AreEqual(_sliderervice.GetById(slider.Id).Name, slider.Name);
		}
		[Test]
		public void Delete_PasValidId_DeleteSocialMedia()
		{
			var slider = CreateSocialMedia();
			var result = _sliderController.Delete(slider.Id);

			Assert.IsFalse(_sliderervice.Exists(i => i.Id == slider.Id));
		}
		#region HelperMethods
		private SocialMediaDTO CreateSocialMedia()
		{
			var slider = new SocialMediaDTO()
			{
				Name = "test",
				Link = "#",
				ImagePath = "no image",
				IsActive = true
			};
			_sliderervice.Add(slider);
			return slider;
		}
		#endregion
	}
}
