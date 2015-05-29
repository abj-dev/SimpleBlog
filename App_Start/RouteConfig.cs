using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SimpleBlog.Controllers;

namespace SimpleBlog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            String[] _nameSpaces = new String[] { typeof(PostsController).Namespace };

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //**Note - Commented out default route to gain more granular control over website routing.
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            routes.MapRoute("Logout", "Logout", new { controller = "Authentication", action = "Logout" }, _nameSpaces);
            routes.MapRoute("Login", "Login", new { controller = "Authentication", action = "Login" }, _nameSpaces);

            routes.MapRoute("Home", "", new { controller = "Posts", action = "Index" }, _nameSpaces);

            routes.MapRoute("PostRealRedirectUrl", "post/{idAndSlug}", new { controller = "Posts", action = "Show" }, _nameSpaces);
            routes.MapRoute("Post", "Post/{Id}-{slug}", new {controller = "Posts", action = "Show"}, _nameSpaces);

            routes.MapRoute("TagRealRedirectUrl", "tag/{idAndSlug}", new { controller = "Posts", action = "SelectTag" }, _nameSpaces);
            routes.MapRoute("Tag", "Tag/{Id}-{slug}", new { controller = "Posts", action = "SelectTag" }, _nameSpaces);
        }
    }
}
