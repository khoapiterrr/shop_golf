using System.Web.Optimization;

namespace Stanford_ShopGolf
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                "~/Scripts/knockout-{version}.js",
                "~/Scripts/knockout.validation.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                  "~/Scripts/bootstrap.min.js", "~/Scripts/bootstrap.bundle.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/bootstrap-grid.min.css",
                "~/Content/bootstrap-reboot.min.css",
                "~/Content/font-icons.css",
                "~/Content/slick.css",
                "~/Content/slick-theme.css",
                "~/Content/jquery.rateyo.min.css",
                "~/Content/glasscase.min.css",
                "~/Content/Site.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/hero-slider").Include(
                "~/Scripts/slick.min.js",
                "~/Scripts/hero-slideshow.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/quickviewproduct").Include("~/Scripts/quickviewproduct.js"));
            bundles.Add(new ScriptBundle("~/bundles/rate").Include("~/Scripts/jquery.rateyo.min.js", "~/Scripts/rate.bind.js"));
            bundles.Add(new ScriptBundle("~/bundles/zoommer").Include("~/Scripts/jquery.glasscase.min.js", "~/Scripts/modernizr.custom.js", "~/Scripts/product.detail.script.js"));
        }
    }
}