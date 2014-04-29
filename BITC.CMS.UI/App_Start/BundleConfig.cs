using System.Web;
using System.Web.Optimization;

namespace BITC.CMS.UI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Admin/css/global")
                .Include("~/Areas/Admin/assets/plugins/font-awesome/css/font-awesome.css", new CssRewriteUrlTransform())
                .Include("~/Areas/Admin/assets/plugins/bootstrap/css/bootstrap.css", new CssRewriteUrlTransform())
                .Include("~/Areas/Admin/assets/plugins/uniform/css/uniform.default.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/Admin/css/theme")
                .Include("~/Areas/Admin/assets/css/style-metronic.css", new CssRewriteUrlTransform())
                .Include("~/Areas/Admin/assets/css/style.css", new CssRewriteUrlTransform())
                .Include("~/Areas/Admin/assets/css/style-responsive.css", new CssRewriteUrlTransform())
                .Include("~/Areas/Admin/assets/css/plugins.css", new CssRewriteUrlTransform())
                .Include("~/Areas/Admin/assets/css/themes/default.css", new CssRewriteUrlTransform())
                .Include("~/Areas/Admin/assets/css/custom.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/Admin/js/global")
                .Include("~/Areas/Admin/assets/plugins/jquery-1.10.2.min.js")
                .Include("~/Areas/Admin/assets/plugins/jquery-migrate-1.2.1.min.js")
                .Include("~/Areas/Admin/assets/plugins/bootstrap/js/bootstrap.min.js")
                .Include("~/Areas/Admin/assets/plugins/bootstrap-hover-dropdown/twitter-bootstrap-hover-dropdown.min.js")
                .Include("~/Areas/Admin/assets/plugins/jquery-slimscroll/jquery.slimscroll.min.js")
                .Include("~/Areas/Admin/assets/plugins/jquery.blockui.min.js")
                .Include("~/Areas/Admin/assets/plugins/jquery.cookie.min.js")
                .Include("~/Areas/Admin/assets/plugins/uniform/jquery.uniform.min.js")
                );


            bundles.Add(new StyleBundle("~/kendoui/css")
                .Include("~/Content/kendo/2014.1.318/kendo.common.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/kendo/2014.1.318/kendo.dataviz.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/kendo/2014.1.318/kendo.bootstrap.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/kendo/2014.1.318/kendo.dataviz.bootstrap.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/kendoui/js")
                .Include("~/Scripts/kendo/2014.1.318/kendo.all.min.js")
                .Include("~/Scripts/kendo/2014.1.318/kendo.aspnetmvc.min.js")
                .Include("~/Scripts/kendo/2014.1.318/kendo.modernizr.custom.js")
                );

            BundleTable.EnableOptimizations = false;
        }
    }
}
