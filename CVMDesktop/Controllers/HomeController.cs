using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CVMDesktop.dbModel;
using Microsoft.AspNetCore.Mvc;
using YesSql;

namespace CVMDesktop.Controllers
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
