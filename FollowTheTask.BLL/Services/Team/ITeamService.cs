using System.Threading.Tasks;
using FollowTheTask.BLL.Result;
using FollowTheTask.BLL.Services.Feature.ViewModels;
using FollowTheTask.BLL.Services.Model;
using FollowTheTask.BLL.Services.Team.ViewModels;
using FollowTheTask.TransferObjects.Feature.DataObjects;
using FollowTheTask.TransferObjects.Team.DataObjects;
using FollowTheTask.TransferObjects.Team.Queries;

namespace FollowTheTask.BLL.Services.Team
{
    public interface ITeamService : IModelService<TeamDto, TeamViewModel>
    {
        QueryResult<TeamInfoDto> GetTeamDto(TeamQuery query);

        Task<QueryResult<TeamInfoDto>> GetTeamDtoAsync(TeamQuery query);

        QueryResult<TeamInfoViewModel> GetTeam(TeamQuery query);

        Task<QueryResult<TeamInfoViewModel>> GetTeamAsync(TeamQuery query);

        ListQueryResult<TeamMemberDto> GetTeamMembersDtos(TeamMembersQuery query);

        Task<ListQueryResult<TeamMemberDto>> GetTeamMembersDtosAsync(TeamMembersQuery query);

        ListQueryResult<TeamMemberViewModel> GetTeamMembers(TeamMembersQuery query);

        Task<ListQueryResult<TeamMemberViewModel>> GetTeamMembersAsync(TeamMembersQuery query);

        ListQueryResult<FeatureInfoDto> GetTeamFeaturesDtos(TeamFeaturesQuery query);

        Task<ListQueryResult<FeatureInfoDto>> GetTeamFeaturesDtosAsync(TeamFeaturesQuery query);

        ListQueryResult<FeatureInfoViewModel> GetTeamFeatures(TeamFeaturesQuery query);

        Task<ListQueryResult<FeatureInfoViewModel>> GetTeamFeaturesAsync(TeamFeaturesQuery query);

        ListQueryResult<TeamInfoDto> GetAllTeamsDtos(AllTeamsQuery query);

        Task<ListQueryResult<TeamInfoDto>> GetAllTeamsDtosAsync(AllTeamsQuery query);

        ListQueryResult<TeamInfoViewModel> GetAllTeams(AllTeamsQuery query);

        Task<ListQueryResult<TeamInfoViewModel>> GetAllTeamsAsync(AllTeamsQuery query);
    }
}