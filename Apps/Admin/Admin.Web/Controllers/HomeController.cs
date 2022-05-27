using Microsoft.AspNetCore.Mvc;

namespace Admin.Web.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
