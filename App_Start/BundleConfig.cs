using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Optimization;

namespace SimpleBlog
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Frontend/Styles")
                .Include("~/Content/Styles/bootstrap.css")
                .Include("~/Content/Styles/Site.css"));

            bundles.Add(new StyleBundle("~/Admin/Styles")
                .Include("~/Content/Styles/bootstrap.css")
                .Include("~/Content/Styles/Admin.css"));

            bundles.Add(new ScriptBundle("~/Frontend/Scripts")
                .Include("~/Scripts/jquery-{version}.js")
                .Include("~/Scripts/jquery.validate.js")
                .Include("~/Scripts/jquery.validate.unobtrusive.js")
                .Include("~/Scripts/jquery.timeago.js")
                .Include("~/Scripts/frontend.js")
                .Include("~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/Admin/Scripts")
                .Include("~/Scripts/jquery-{version}.js")
                .Include("~/Scripts/jquery.validate.js")
                .Include("~/Scripts/jquery.validate.unobtrusive.js")
                .Include("~/areas/admin/scripts/forms.js")
                .Include("~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/Admin/Post/Scripts")
                .Include("~/areas/admin/scripts/Post_TagEditor.js"));
        }
    }
}
