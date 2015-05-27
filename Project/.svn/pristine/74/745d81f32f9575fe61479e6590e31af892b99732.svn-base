using System.Web;
using System.Web.Optimization;

namespace LazyAtHome.Web.WebManage
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/Jquery/jquery-1.7.2.js",
                        "~/Scripts/Jquery/jquery.cookie.js",
                        "~/Scripts/Jquery/jquery.validate.js",
                        "~/Scripts/Jquery/jquery.bgiframe.js"));

            bundles.Add(new ScriptBundle("~/bundles/dwz").Include(
                      "~/Scripts/Dwz/dwz.min.js",
                      "~/Scripts/Dwz/dwz.regional.zh.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/themes/default/style.css",
                "~/Content/themes/css/core.css"));

            bundles.Add(new StyleBundle("~/bundles/myjs").Include(
                "~/Scripts/Common/Division.js",
                "~/Scripts/Common/WashClass.js"));
        }
    }
}
