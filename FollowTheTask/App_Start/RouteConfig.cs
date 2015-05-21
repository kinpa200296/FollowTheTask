using System.Web.Mvc;
using System.Web.Routing;

namespace FollowTheTask
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Roles", "role/{name}/{action}", new {controller = "Roles", action = "Details"},
                new {action = "^[^I].*"});

            routes.MapRoute("ManageUser", "user/{username}/manage/{action}",
                new {controller = "ManageAccount", action = "Index"});

            routes.MapRoute("ShowUser", "user/{username}/{action}", new {controller = "User", action = "Index"});

            routes.MapRoute("UsersManagement", "users/{username}/{action}", new {controller = "Users", action = "Index"},
                new { action = "^[^I].*" });

            routes.MapRoute("Default", "{controller}/{action}/{id}",
                new {controller = "Main", action = "Index", id = UrlParameter.Optional});
        }
    }
}
