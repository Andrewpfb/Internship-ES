using System.Web;
using System.Web.Optimization;

namespace MapsProject.WEB
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/lib/JQuery/jquery-{version}.js",
                        "~/Content/lib/DataTables/js/*.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Content/lib/JQuery/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Content/lib/Modernizr/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/lib/Bootstrap/bootstrap.js",
                       "~/Content/lib/DataTables/js/*.js", //HERE
                      "~/Content/lib/Bootstrap/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/lib/Bootstrap/bootstrap.css",
                      "~/Content/styles/site.css"));

            //bundles.Add(new ScriptBundle("~/bundles/dataTablesScript").Include(
            //    "~/Content/lib/DataTables/js/*.js"));

            //bundles.Add(new StyleBundle("~/Content/dataTablesCss").Include(
            //               "~/Content/lib/DataTables/css/*.css"));
        }
    }
}
