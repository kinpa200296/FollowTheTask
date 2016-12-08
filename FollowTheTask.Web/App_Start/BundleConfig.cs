using System.Web.Optimization;
using BundleTransformer.Core.Bundles;

namespace FollowTheTask.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/scripts/vendor").Include(
                      "~/Scripts/jquery-{version}.js",
                      "~/Scripts/jquery.validate*",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new CustomStyleBundle("~/styles").Include(
                      "~/Content/style/main.less"));
        }
    }
}