using System.Web;
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

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Получаем куки из контекста, которые могут содержать установленную культуру
            HttpCookie cultureCookie = filterContext.HttpContext.Request.Cookies["lang"];
            if (cultureCookie != null)
                ViewBag.Language = cultureCookie.Value;
            else
                ViewBag.Language = "ru";
            base.OnActionExecuting(filterContext);
        }
    }
}