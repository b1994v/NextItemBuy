using System.Web;
using System.Web.Optimization;

namespace NextItemBuy.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/scripts").Include(
                    "~/bundles/runtime.*",
                    "~/bundles/polyfills.*",
                    "~/bundles/main.*"
                    ));

            bundles.Add(new StyleBundle("~/styles-css").Include(
                      "~/fonts/font-awesome.css",
                      "~/content/bootstrap.css",
                      "~/bundles/styles.*"));
        }
    }
}
