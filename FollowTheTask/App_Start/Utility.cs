using System;
using System.Linq;
using System.Web.Mvc;

namespace FollowTheTask
{
    public static class Utility
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

        public static string GetStatus(DateTime deadline, DateTime completed, bool finished)
        {
            if (finished)
            {
                return completed > deadline ? "Завершено невовремя" : "Завершено вовремя";
            }
            if (deadline < DateTime.Now)
                return "Срок сдачи просрочен";
            var span = deadline - DateTime.Now;
            return span.Days > 3 ? "Выполняется" : "Срок сдачи близок";
        }

        public static string GetStatusCssClass(DateTime deadline, DateTime completed, bool finished)
        {
            if (finished)
            {
                return completed > deadline ? "" : "alert-success";
            }
            if (deadline < DateTime.Now)
                return "alert-danger";
            var span = deadline - DateTime.Now;
            return span.Days > 3 ? "alert-info" : "alert-warning";
        }
    }
}