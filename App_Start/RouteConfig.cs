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
            String[] _NameSpaces = new String[] { typeof(PostsController).Namespace };

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            //**Note - Commented out default route to gain more granular control over website routing.
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            routes.MapRoute("Logout", "Logout", new { controller = "Authentication", action = "Logout" }, _NameSpaces);

            routes.MapRoute("Login", "Login", new { controller = "Authentication", action = "Login" }, _NameSpaces);

            routes.MapRoute("Home", "", new { controller = "Posts", action = "Index" }, _NameSpaces);
        }
    }
}
