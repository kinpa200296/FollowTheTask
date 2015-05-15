using System.Linq;
using System.Web.Mvc;

namespace FollowTheTask
{
    public static  class Utility
    {
        public static string GetCallbackUrl(UrlHelper url, string actionName, string controllerName, object routeValues,
            string protocol, string hostname)
        {
            var result = url.Action(actionName, controllerName, routeValues, protocol);
            if (hostname == "localhost") return result;
            var s = result.Split('/').First(x => x.Contains(hostname));
            result = result.Replace(s, hostname);
            return result;
        }
    }
}