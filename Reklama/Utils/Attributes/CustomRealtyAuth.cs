﻿using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reklama.Attributes
{
    public class CustomRealtyAuth : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return ProjectConfiguration.IsAnonymousUserAllowed || base.AuthorizeCore(httpContext);
        }
    }
}