using System.Web;
using System.Web.Optimization;


namespace Reklama.App_Start
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui-1.12.1").Include(
                        "~/Scripts/jquery-ui-1.12.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/jquery.mCustomScrollbar.css",
                "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/angular-file-uploader")
                .Include("~/Scripts/angular/angular.min.js")
                .Include("~/Scripts/angular/angular-file-upload.js")
                .Include("~/Scripts/angular/controllers.js")
                .Include("~/Scripts/angular/directives.js")
                .Include("~/Scripts/angular/es5-shim.min.js"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));

            bundles.Add(new StyleBundle("~/Content/mobile/less")
                .Include(
                   "~/Scripts/mobile/bootstrap-3.3.5/less/bootstrap.less",
                   "~/Content/mobile/mobile.less"
                )); 
            
            bundles.Add(new ScriptBundle("~/Scripts/mobile/js")
                .Include(
                   "~/Scripts/mobile/jquery-1.11.3.min.js",
                   "~/Scripts/mobile/bootstrap-3.3.5/dist/js/bootstrap.min.js",
                   "~/Scripts/mobile/angular.min.js",
                   "~/Scripts/mobile/advPage.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/js/bootstrap")
                .Include(
                    "~/Scripts/jquery-3.0.0.min.js",
                    "~/Scripts/umd/popper.min.js",
                    "~/Scripts/bootstrap.min.js"
                ));

            bundles.Add(new StyleBundle("~/bundles/styles/bootstrap")
               .Include(
                   "~/Content/bootstrap.min.css"
               ));
            bundles.Add(new StyleBundle("~/bundles/styles/main-mobile")
               .Include(
                   "~/Content/main-mobile.css"
               ));
            bundles.Add(new StyleBundle("~/bundles/styles/fontawesome").Include("~/Content/fontawesome-all.min.css", new CssRewriteUrlTransform()));
        }

        public static void RegisterMobileBundles(BundleCollection bundles)
        {
            bundles.Add(new LessBundle("~/Content/mobile/common/less")
               .Include(
                  "~/Scripts/mobile/bootstrap-3.3.5/less/bootstrap.less",
                  "~/Content/mobile/bootstrap-select.css",
                  "~/Content/mobile/touch-carousel.css",
                  "~/Content/mobile/mobile.less"
               ));

            bundles.Add(new ScriptBundle("~/Scripts/mobile/common/js")
                .Include(
                   "~/Scripts/mobile/jquery-1.11.3.min.js",
                   "~/Scripts/mobile/jquery.tmpl.js",
                   "~/Scripts/mobile/js.cookie.js",
                   "~/Scripts/mobile/jquery.maskedinput.js",
                   "~/Scripts/mobile/jquery.timeago.js",
                   "~/Scripts/mobile/jquery.timeago.ru.js",
                   "~/Scripts/mobile/touch-carusel.js",
                   "~/Scripts/mobile/bootstrap-3.3.5/dist/js/bootstrap.min.js",
                   "~/Scripts/mobile/bootstrap-select.js",
                   "~/Scripts/mobile/extentions.js",
                   "~/Scripts/mobile/advPage.js"
                ));
        }
    }
}