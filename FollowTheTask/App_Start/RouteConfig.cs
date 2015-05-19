using System.Web.Mvc;
using System.Web.Routing;

namespace FollowTheTask
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute("UserAnonymous", "user/{action}", new {controller = "Account"});

            routes.MapRoute("Roles", "role/{name}/{action}", new {controller = "Roles", action = "Details"},
                new {action = "^[^I].*"});

            routes.MapRoute("Default", "{controller}/{action}/{id}",
                new {controller = "Main", action = "Index", id = UrlParameter.Optional});
        }
    }
}
