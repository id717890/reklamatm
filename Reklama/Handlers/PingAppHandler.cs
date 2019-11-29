using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reklama.Handlers
{
    public class PingAppHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            HttpResponse Response = context.Response;
            // This handler is called whenever a file ending 
            // in .sample is requested. A file with that extension
            // does not need to exist.
            Response.Write("<html>");
            Response.Write("<body>");
            Response.Write(string.Format("<h1>Ping Successfully, time:{0} </h1>",DateTime.Now));
            Response.Write("</body>");
            Response.Write("</html>");
        }

        public bool IsReusable { get { return false; } }
    }
}