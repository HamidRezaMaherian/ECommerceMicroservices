using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UI.API.Controllers;
using UI.API.Configurations.DTOs;
using UI.Application.Services;
using UI.Domain.Entities;
using static UI.API.Tests.Utils.TestUtilsExtension;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.IO;

namespace UI.API.Tests.Unit
{
	[TestFixture]
	public class SliderControllerTests
	{
		private ISliderService _sliderervice;
		private SliderController _sliderController;
		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			var _sliders = new List<Slider>();
			_sliderervice = MockAction<Slider, Application.DTOs.SliderDTO>
				.MockServie<ISliderService>(_sliders).Object;
			_sliderController = new SliderController(_sliderervice);
		}
		[Test]
		public void GetAll_ReturnAllUIs()
		{
			var slider = CreateSlider();
			var res = _sliderController.GetAll();
			CollectionAssert.AreEquivalent(res.Value?.Select(i => i.Id), _sliderervice.GetAll().Select(i => i.Id));
		}
		[Test]
		public void Create_PasValidEntity_AddSlider()
		{
			var slider = new CreateSliderDTO()
			{
				IsActive = true,
				Title = "no title"
			};
			_sliderController.Create(slider);
			Assert.IsNotNull(_sliderervice.GetById(slider.Id));
		}
		[Test]
		public void Update_PasValidEntity_UpdateSlider()
		{
			var slider = CreateSlider();
			slider.Title = "updatedTest";
			_sliderController.Update(slider);

			Assert.AreEqual(_sliderervice.GetById(slider.Id).Title, slider.Title);
		}
		[Test]
		public void Delete_PasValidId_DeleteSlider()
		{
			var slider = CreateSlider();
			var result = _sliderController.Delete(slider.Id);

			Assert.IsFalse(_sliderervice.Exists(i => i.Id == slider.Id));
		}

		[Test]
		public void Get_PassValidId_ReturnEntity()
		{
			var slider = CreateSlider();
			var result = _sliderController.Get(slider.Id);

			Assert.IsNotNull(result);
			Assert.AreEqual(result.Value?.Id,slider.Id);
		}
		#region HelperMethods
		private IFormFile MockFormFile()
		{
			var content = Encoding.UTF8.GetBytes("sdfsdf");
			var file = new FormFile(new MemoryStream(content), 0, content.Length, "data", "fake.jpg");
			file.Headers = new HeaderDictionary();
			file.ContentType = "image/jpg";
			return file;
		}

		private UpdateSliderDTO CreateSlider()
		{
			var slider = new UpdateSliderDTO()
			{
				ImagePath = "no image",
				Title = "no title",
				IsActive = true
			};
			_sliderervice.Add(slider);
			return slider;
		}
		#endregion
	}
}
