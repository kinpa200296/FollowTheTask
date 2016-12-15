using System.Collections.Generic;
using FollowTheTask.BLL.Services.Request.ViewModels;

namespace FollowTheTask.Web.Infrastructure
{
    public static class RequestHelper
    {
        public static Dictionary<int, string> MessageFormats = new Dictionary<int, string>
        {
            {1, "{0} wants to be a leader of team {1}"},
            {2, "{0} wants to join team {1}"},
            {3, "{0} wants to be assigned to issue {1}"}
        };


        public static string GetMessage(RequestInfoViewModel model)
        {
            try
            {
                return string.Format(MessageFormats[model.ActionTypeId], model.Sender, model.Target);
            }
            catch
            {
                return $"{model.Sender} {StaticValues.Instance.ActionTypes[model.ActionTypeId]} {model.Target}";
            }
        }
    }
}