using System.Web.Optimization;

namespace MapsProject.WEB
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/lib/JQuery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Content/lib/JQuery/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Content/lib/Modernizr/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/lib/Bootstrap/bootstrap.js",
                      "~/Content/lib/Bootstrap/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/lib/Bootstrap/bootstrap.css",
                      "~/Content/styles/site.css"));

            // DataTables plugin bundle.
            bundles.Add(new ScriptBundle("~/bundles/dataTables").Include(
                "~/Content/lib/DataTables/js/*.js"));

            bundles.Add(new StyleBundle("~/Content/dataTables").Include(
                           "~/Content/lib/DataTables/css/*.css"));

            // Select2 plugin bundle.
            bundles.Add(new ScriptBundle("~/bundles/select2").Include(
                "~/Content/lib/Select2/js/*.js"));

            bundles.Add(new StyleBundle("~/Content/select2").Include(
                "~/Content/lib/Select2/css/*.css"));
        }
    }
}
