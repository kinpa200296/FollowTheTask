using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.Feature.DataObjects;
using FollowTheTask.TransferObjects.Team.DataObjects;
using FollowTheTask.TransferObjects.Team.Queries;

namespace FollowTheTask.DAL.Repositories.Team
{
    public interface ITeamRepository : IModelRepository<TeamDto>,
        IQueryRepository<TeamQuery, TeamInfoDto>,
        IListQueryRepository<TeamMembersQuery, TeamMemberDto>,
        IListQueryRepository<TeamFeaturesQuery, FeatureInfoDto>
    {
    }
}