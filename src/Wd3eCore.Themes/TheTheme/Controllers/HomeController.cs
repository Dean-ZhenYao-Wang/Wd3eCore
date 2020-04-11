using Microsoft.AspNetCore.Mvc;
using YesSql;

namespace Wd3eCore.TheTheme.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISession _session;
        public HomeController(ISession session)
        {
            _session = session;
        }
        public IActionResult Index()
        {
            string ConnectionString = _session.Store.Configuration.ConnectionFactory.CreateConnection().ConnectionString;
            string TablePrefix = _session.Store.Configuration.TablePrefix;
            return View();
        }
        public IActionResult Welcome()
        {
            return View();
        }
    }
}
