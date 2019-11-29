using System.Web.Mvc;

namespace Reklama.Controllers
{
    public class _BaseController : Controller
    {
        protected bool IsMobileDevice ()
        {
            return true;
            //return Request.Browser.IsMobileDevice;
        }
    }
}