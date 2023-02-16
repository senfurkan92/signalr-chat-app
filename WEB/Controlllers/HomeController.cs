using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WEB.Controlllers
{
    [Authorize]
    public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return RedirectToAction("Index", "Chat");
		}
	}
}
