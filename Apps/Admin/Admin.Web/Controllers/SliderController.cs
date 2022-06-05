using Admin.Application.Services.UI;
using Admin.Application.UnitOfWork.UI;
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
		public async Task<IActionResult> Index()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create()
		{
			return View("Form");
		}
		[HttpGet]
		public async Task<IActionResult> GetData()
		{
			return Json(await _uiUnit.Slider.GetAllAsync());

		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(string id)
		{
			await _uiUnit.Slider.DeleteAsync(id);
			return Ok();
		}
	}
}
