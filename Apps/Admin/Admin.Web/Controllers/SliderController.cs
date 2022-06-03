using Microsoft.AspNetCore.Mvc;

namespace Admin.Web.Controllers
{
	[AutoValidateAntiforgeryToken]
	public class SliderController : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Insert()
		{

		}
	}
}
