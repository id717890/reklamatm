using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reklama.Controllers
{
    public class Home1Controller : Controller
    {
        // GET: Home1
        public ActionResult Index()
        {
            return File(Server.MapPath("/testmakeup/home1/") + "index.html", "text/html");
        }
    }
}