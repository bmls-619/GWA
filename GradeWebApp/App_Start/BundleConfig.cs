using System.Web;
using System.Web.Optimization;

namespace GradeWebApp
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-2.1.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/jquery2").Include(
            //            "~/Scripts/jquery-2.1.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                     "~/Scripts/bootstrap.js",
                     "~/Scripts/respond.js",
                     "~/Scripts/bootstrap-hover-dropdown.js"));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                        "~/Scripts/site.js"));

            bundles.Add(new ScriptBundle("~/bundle/bootbox").Include(
                    "~/Scripts/bootbox.min.js"));

            bundles.Add(new ScriptBundle("~/bundle/v").Include(
                    "~/Scripts/v.js"));

            bundles.Add(new ScriptBundle("~/bundle/metisMenu").Include(
                    "~/Content/bower_components/metisMenu/dist/metisMenu.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/dataTables").Include(
                       "~/Content/bower_components/DataTables/media/js/jquery.dataTables.min.js",
                       "~/Content/bower_components/datatables-plugins/integration/bootstrap/3/dataTables.bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/gmap").Include(
                       "~/Scripts/jquery.gmap.js",
                       "~/Scripts/jquery.gmap_init.js"));

            bundles.Add(new ScriptBundle("~/bundles/mask").Include(
                        "~/Scripts/jquery.mask.js",
                        "~/Scripts/v2.js"));

            bundles.Add(new ScriptBundle("~/bundles/modalform").Include(
                        "~/Scripts/modalform.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/dataTablesCSS").Include(
                      "~/Content/bower_components/datatables-plugins/integration/bootstrap/3/dataTables.bootstrap.css",
                      "~/Content/bower_components/datatables-responsive/css/dataTables.responsive.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}