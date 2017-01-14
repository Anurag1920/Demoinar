using System.Web;
using System.Web.Optimization;

namespace Webinar.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                //"~/Scripts/jquery-{version}.js",
                         "~/Scripts/js/jquery.js",
                         "~/Scripts/js/jquery.easing.min.js",
                         "~/Scripts/js/jquery.fittext.js",
                         "~/Scripts/js/wow.min.js"
                //"~/Scripts/js/creative.js",
                //"~/Scripts/js/bootstrap.min.js"
                         ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/css/Style.css",
                "~/Content/css/layerslider.css",
                "~/Content/css/hover.css",
               "~/Content/css/full-slider.css",
               "~/Content/css/animate.min.css"
                ));


            bundles.Add(new StyleBundle("~/bundles/bootstrap").Include(

              "~/Content/css/bootstrap.min.css"

               ));
        }
    }
}