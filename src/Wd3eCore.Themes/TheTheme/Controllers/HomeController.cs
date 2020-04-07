using Microsoft.AspNetCore.Mvc;

namespace Wd3eCore.TheTheme.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Welcome()
        {
            return View();
        }
    }
}
