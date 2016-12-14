using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FollowTheTask.BLL.Services.Team;
using FollowTheTask.BLL.Services.User;
using FollowTheTask.TransferObjects.Team.Queries;
using FollowTheTask.TransferObjects.User.Queries;

namespace FollowTheTask.Web.Infrastructure
{
    public static class PermissionResolver
    {
        public static bool IsAdmin(int userId)
        {
            return userId == 1;
        }

        #region TeamPermissions

        public static bool TeamEditPermission(int userId, int teamId)
        {
            var model = DependencyResolver.Current.GetService<ITeamService>().GetTeam(new TeamQuery() {Id = teamId}).Value;
            return model.LeaderId == userId || IsAdmin(userId);
        }

        #endregion
    }
}