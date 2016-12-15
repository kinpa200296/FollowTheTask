using System.Linq;
using System.Web.Mvc;
using FollowTheTask.BLL.Services.Feature;
using FollowTheTask.BLL.Services.Issue;
using FollowTheTask.BLL.Services.Team;
using FollowTheTask.TransferObjects.Feature.Queries;
using FollowTheTask.TransferObjects.Issue.Queries;
using FollowTheTask.TransferObjects.Team.Queries;

namespace FollowTheTask.Web.Infrastructure
{
    public static class PermissionResolver
    {
        #region Role check

        public static bool IsAdmin(int userId)
        {
            return userId == 1;
        }

        public static bool IsTeamLeader(int userId, int teamId)
        {
            var model = DependencyResolver.Current.GetService<ITeamService>().GetTeam(new TeamQuery() { Id = teamId }).Value;
            return model?.LeaderId == userId;
        }

        public static bool IsTeamMember(int userId, int teamId)
        {
            var members =
                DependencyResolver.Current.GetService<ITeamService>()
                    .GetTeamMembers(new TeamMembersQuery {TeamId = teamId})
                    .Value?.ToList();
            return members?.Any(it => it.Id == userId) ?? false;
        }

        public static bool IsIssueReporter(int issueId, int userId)
        {
            var model =
                DependencyResolver.Current.GetService<IIssueService>().GetIssue(new IssueQuery() { Id = issueId }).Value;
            return model?.ReporterId != null && model.ReporterId == userId;
        }
        public static bool IsIssueAssignee(int issueId, int userId)
        {
            var model =
                DependencyResolver.Current.GetService<IIssueService>().GetIssue(new IssueQuery() { Id = issueId }).Value;
            return model?.AssigneeId != null && model.AssigneeId == userId;
        }

        #endregion

        #region TeamPermissions

        public static bool TeamEditPermission(int userId, int teamId)
        {
            return IsAdmin(userId) || IsTeamLeader(userId, teamId);
        }

        public static bool TeamJoinPermision(int userId, int teamId)
        {
            return !IsTeamMember(userId, teamId) && !IsTeamLeader(userId, teamId);
        }

        #endregion

        #region IssuePermission

        public static bool IssueEditGeneralPermission(int userId, int issueId)
        {
            return IsAdmin(userId) || IsTeamLeader(userId, GetTeamLeaderIdForIssue(issueId) ?? 0) ||
                   IsIssueReporter(issueId, userId) || IsIssueAssignee(issueId, userId);
        }

        public static bool IssueEditNoRepPermission(int userId, int issueId)
        {
            return IsAdmin(userId) || IsTeamLeader(userId, GetTeamLeaderIdForIssue(issueId) ?? 0) ||
                   IsIssueAssignee(issueId, userId);
        }

        public static bool IssueEditNoAssigneePermission(int userId, int issueId)
        {
            return IsAdmin(userId) || IsTeamLeader(userId, GetTeamLeaderIdForIssue(issueId) ?? 0) ||
                   IsIssueReporter(issueId, userId);
        }

        public static bool IssueDeletePermission(int userId, int issueId)
        {
            return IsAdmin(userId) || IsTeamLeader(userId, GetTeamLeaderIdForIssue(issueId) ?? 0);
        }

        public static bool IssueAssignPermission(int userId, int issueId)
        {
            return !IsIssueAssignee(issueId, userId) && IsTeamMember(userId, GetTeamIdForIssue(issueId) ?? 0);
        }


        #endregion

        #region Private methods

        private static int? GetTeamLeaderIdForIssue(int issueId)
        {
            var featureId = DependencyResolver.Current.GetService<IIssueService>()
                .GetIssue(new IssueQuery() {Id = issueId})
                .Value?.FeatureId;
            if (featureId == null) return null;
            var teamId =
                DependencyResolver.Current.GetService<IFeatureService>()
                    .GetFeature(new FeatureQuery() {Id = featureId.Value}).Value?.TeamId;
            if (teamId == null) return null;
            return
                DependencyResolver.Current.GetService<ITeamService>()
                    .GetTeam(new TeamQuery() {Id = teamId.Value})
                    .Value?.LeaderId;
        }

        private static int? GetTeamIdForIssue(int issueId)
        {
            var featureId = DependencyResolver.Current.GetService<IIssueService>()
                .GetIssue(new IssueQuery() { Id = issueId })
                .Value?.FeatureId;
            if (featureId == null) return null;
            return DependencyResolver.Current.GetService<IFeatureService>()
                .GetFeature(new FeatureQuery() {Id = featureId.Value}).Value?.TeamId;
        }

        #endregion
    }
}