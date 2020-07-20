using System.Web;
using System.Web.Optimization;

namespace DoAn
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));

            /////-----------------------------------/

            bundles.Add(new StyleBundle("~/freecss").Include(
                     "~/css/bootstrap.min.css",
                     "~/css/style.css",
                     "~/css/responsive.css",
                     "~/css/custom.css"));
            bundles.Add(new StyleBundle("~/freecss2").Include(
                   "~/css/bootstrap.min.css",
                   "~/css/darkmode.css",
                   "~/css/responsive.css",
                   "~/css/custom.css"));

            bundles.Add(new ScriptBundle("~/freejs").Include(
                     "~/js/jquery-3.2.1.min.js",
                     "~/js/popper.min.js",
                     "~/js/bootstrap.min.js",
                     "~/js/jquery.superslides.min.js",
                     "~/js/bootstrap-select.js",
                     "~/js/inewsticker.js",
                     "~/js/bootsnav.js",
                     "~/js/images-loded.min.js",
                     "~/js/isotope.min.js",
                     "~/js/owl.carousel.min.js",
                     "~/js/baguetteBox.min.js",
                     "~/js/form-validator.min.js",
                     "~/js/contact-form-script.js",
                     "~/js/custom.js",
                     "~/js/theme.js"));
        }
    }
}
