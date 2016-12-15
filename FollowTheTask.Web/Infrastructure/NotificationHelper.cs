using System;
using System.Collections.Generic;
using FollowTheTask.BLL.Services.Notification.ViewModels;

namespace FollowTheTask.Web.Infrastructure
{
    public static class NotificationHelper
    {
        public static Dictionary<Tuple<int, int>, string> MessageFormats = new Dictionary<Tuple<int, int>, string>
        {
            {new Tuple<int, int>(1, 1), "{0} made you a leader of team {1}"},
            {new Tuple<int, int>(2, 1), "{0} approved your request to be a leader of team {1}"},
            {new Tuple<int, int>(3, 1), "{0} declined your request to be a leader of team {1}"},
            {new Tuple<int, int>(1, 2), "{0} joined you to team {1}"},
            {new Tuple<int, int>(2, 2), "{0} approved your request to join team {1}"},
            {new Tuple<int, int>(3, 2), "{0} declined your request to join team {1}"},
            {new Tuple<int, int>(1, 3), "{0} assigned you to issue {1}"},
            {new Tuple<int, int>(2, 3), "{0} approved your request to assign issue {1}"},
            {new Tuple<int, int>(3, 3), "{0} declined your request to assign issue {1}"},
        };


        public static string GetMessage(NotificationInfoViewModel model)
        {
            try
            {
                return string.Format(MessageFormats[new Tuple<int, int>(model.ActionSourceId, model.ActionTypeId)],
                    model.Sender, model.Target);
            }
            catch
            {
                return $"{model.Sender} {StaticValues.Instance.ActionSources[model.ActionSourceId]} " +
                       $"{StaticValues.Instance.ActionTypes[model.ActionTypeId]} {model.Target}";
            }
        }
    }
}