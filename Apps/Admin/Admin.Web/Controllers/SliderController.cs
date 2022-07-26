using Admin.Application.UnitOfWork.UI;
using Admin.Web.Configurations;
using Admin.Web.ViewModels;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Web.Controllers
{
	[AutoValidateAntiforgeryToken]
	public class SliderController : Controller
	{
		private readonly IUIUnitOfWork _uiUnit;

		public SliderController(IUIUnitOfWork uiUnit)
		{
			_uiUnit = uiUnit;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}
		[HttpGet]
		public IActionResult Create()
		{

			return View("Form", new SliderVM());
		}
		[HttpPost]
		public async Task<IActionResult> CreateAsync(
			[CustomizeValidator(RuleSet = $"{Statics.CREATE_MODEL},{Statics.DEFAULT_MODEL}")] SliderVM modelVM)
		{
			if (!ModelState.IsValid)
				return View("Form", modelVM);

			await _uiUnit.Slider.AddAsync(modelVM);
			return RedirectToAction(nameof(Index));
		}
		[HttpGet]
		public async Task<IActionResult> UpdateAsync(string id)
		{
			return View("Form",await _uiUnit.Slider.GetByIdAsync<SliderVM>(id));
		}
		[HttpPost]
		public async Task<IActionResult> UpdateAsync(
			[CustomizeValidator(RuleSet = $"{Statics.UPDATE_MODEL},{Statics.DEFAULT_MODEL}")] SliderVM modelVM)
		{
			if (!ModelState.IsValid)
				return View("Form", modelVM);

			await _uiUnit.Slider.UpdateAsync(modelVM);
			return RedirectToAction(nameof(Index));
		}
		[HttpGet]
		public async Task<IActionResult> GetData()
		{
			return Json(await _uiUnit.Slider.GetAllAsync());
		}
		[HttpDelete]
		[IgnoreAntiforgeryToken]
		public async Task<IActionResult> Delete(string id)
		{
			await _uiUnit.Slider.DeleteAsync(id);
			return Ok();
		}
	}
}
