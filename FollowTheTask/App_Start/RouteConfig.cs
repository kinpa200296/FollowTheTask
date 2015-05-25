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

            routes.MapRoute("TrackedTasks", "manager/{username}/tasks/{action}/{id}",
                new {controller = "TrackedTasks", action = "Index", id = UrlParameter.Optional});

            routes.MapRoute("Quests", "worker/{username}/quests/{action}/{id}",
                new {controller = "Quests", action = "Index", id = UrlParameter.Optional});

            routes.MapRoute("Managers", "manager/{username}/{action}/{id}",
                new {controller = "Managers", id = UrlParameter.Optional}, new {action = "^[^I].*"});

            routes.MapRoute("Workers", "worker/{username}/{action}/{id}",
                new {controller = "Workers", id = UrlParameter.Optional}, new {action = "^[^I].*"});

            routes.MapRoute("Default", "{controller}/{action}/{id}",
                new {controller = "Main", action = "Index", id = UrlParameter.Optional});
        }
    }
}
