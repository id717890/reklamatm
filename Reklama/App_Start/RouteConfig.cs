namespace Reklama.App_Start
{
    using System.Web.Mvc;
    using System.Web.Routing;

    using Domain.Enums;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(name: "Search", url: "Search", defaults: new { controller = "Search", action = "Search", });

            //routes.Add(new SubdomainRouting());

            routes.MapRoute(
                name: "MyAnnouncements",
                url: "My/Announcements",
                defaults: new { controller = "Bookmarks", action = "MyAnnouncements" });

            routes.MapRoute(
                name: "MyRealty",
                url: "My/Realty",
                defaults: new { controller = "Bookmarks", action = "MyRealty" });

            routes.MapRoute("RealtyAskDelete", "Realty/{action}/{id}", new { controller = "Realty", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "MyAnnouncementBookmarks",
                url: "My/Bookmarks/Announcemets",
                defaults: new { controller = "Bookmarks", action = "Announcements" });

            routes.MapRoute(
                name: "MyRealtyBookmarks",
                url: "My/Bookmarks/Realty",
                defaults: new { controller = "Bookmarks", action = "Realty" });

            routes.MapRoute(
                name: "MyProductBookmarks",
                url: "My/Bookmarks/Products",
                defaults: new { controller = "Bookmarks", action = "Products" });

            routes.MapRoute(
                name: "RestrictedAccess",
                url: "RestrictedAccess.html",
                defaults: new { controller = "Home", action = "RestrictedAccess" });

            routes.Add(
                "DomainRoute",
                new DomainRoute(
                    "jay.reklama.tm",
                    // Domain with parameters
                    "Realty",
                    //Controller name
                    "{action}/{id}",
                    // URL with parameters 
                    new { controller = "Realty", action = "List", id = "" } // Parameter defaults 
                    ));

            #region RedirectToSubdomain

            routes.MapRoute(
                name: "IgnoreRedirectRealtyCreate",
                url: "Realty/Create",
                defaults: new { controller = "Realty", action = "Create" });

            routes.MapRoute(
               name: "IgnoreRedirectRealtyCreateMobile",
               url: "Realty/CreateMobile",
               defaults: new { controller = "Realty", action = "CreateMobile" });

            routes.MapRoute(
                name: "IgnoreRedirectRealtyEdit",
                url: "Realty/Edit/{id}",
                defaults: new { controller = "Realty", action = "Edit", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "RedirectToSubdomain",
                url: "Realty/{actionName}/{id}",
                defaults:
                    new
                    {
                        controller = "Realty",
                        action = "RedirectToSubdomain",
                        id = UrlParameter.Optional,
                        actionName = UrlParameter.Optional
                    });

            #endregion

            //            routes.MapRoute(
            //                "Lb",
            //                "lb/{controller}/{action}/{id}",
            //                new { controller = "Home", action = "Index", id = UrlParameter.Optional, region = Regions.Lb },
            //                new[] { "Reklama.Controllers" }
            //            );
            //
            //            routes.MapRoute(
            //               "Mr",
            //               "mr/{controller}/{action}/{id}",
            //               new { controller = "Home", action = "Index", id = UrlParameter.Optional, region = Regions.Mr },
            //               new[] { "Reklama.Controllers" }
            //           );
            //
            //            routes.MapRoute(
            //               "Bn",
            //               "bn/{controller}/{action}/{id}",
            //               new { controller = "Home", action = "Index", id = UrlParameter.Optional, region = Regions.Bn},
            //               new[] { "Reklama.Controllers" }
            //           );
            //
            //            routes.MapRoute(
            //               "Dz",
            //               "dz/{controller}/{action}/{id}",
            //               new { controller = "Home", action = "Index", id = UrlParameter.Optional, region= Regions.Dz},
            //               new[] { "Reklama.Controllers" }
            //           );

            /*routes.MapRoute(
                "Ag",
                "ag/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional, regions = Regions.Ag },
                new[] { "Reklama.Controllers" });
            */

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                        new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "Reklama.Controllers" }
            );
        }
    }
}