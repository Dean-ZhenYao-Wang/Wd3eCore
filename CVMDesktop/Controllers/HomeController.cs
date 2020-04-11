using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Wd3eCore.CVMDesktop.UnitOfWorks;

namespace Wd3eCore.CVMDesktop.Controllers
{
    public class HomeController : Controller
    {
        private readonly UnitOfWork db;
        public HomeController(CVMDesktop_Context session)
        {
            db = new UnitOfWork(session);
        }
        public IActionResult Index()
        {
            var tt = db.SwaggerUiRepository.Get().FirstOrDefault();
            //var swaggerui = await _session.Query<SwaggerUi,SwaggerUiIndex>().FirstOrDefaultAsync();
            //if (swaggerui == null)
            //{
            //    swaggerui = new SwaggerUi()
            //    {
            //        SID = Guid.NewGuid(),
            //        SwaggerUI_Name = "SwaggerUi"
            //    };
            //    _session.Save(swaggerui);

            //}
            return Ok();
        }
    }
}
