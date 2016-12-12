using System.Threading.Tasks;
using FollowTheTask.BLL.Result;
using FollowTheTask.BLL.Services.Feature.ViewModels;
using FollowTheTask.BLL.Services.Model;
using FollowTheTask.BLL.Services.Team.ViewModels;
using FollowTheTask.DAL.Repositories.Team;
using FollowTheTask.TransferObjects.Feature.DataObjects;
using FollowTheTask.TransferObjects.Team.DataObjects;
using FollowTheTask.TransferObjects.Team.Queries;

namespace FollowTheTask.BLL.Services.Team
{
    public class TeamService : ModelService<TeamDto, TeamViewModel>, ITeamService
    {
        private readonly ITeamRepository _repository;


        public TeamService(ITeamRepository repository) : base(repository)
        {
            _repository = repository;
        }


        public QueryResult<TeamInfoDto> GetTeamDto(TeamQuery query)
        {
            return RunQuery<TeamQuery, TeamInfoDto>(_repository, query);
        }

        public async Task<QueryResult<TeamInfoDto>> GetTeamDtoAsync(TeamQuery query)
        {
            return await RunQueryAsync<TeamQuery, TeamInfoDto>(_repository, query);
        }

        public QueryResult<TeamInfoViewModel> GetTeam(TeamQuery query)
        {
            return RunQuery<TeamQuery, TeamInfoDto>(_repository, query).MapTo<TeamInfoViewModel>();
        }

        public async Task<QueryResult<TeamInfoViewModel>> GetTeamAsync(TeamQuery query)
        {
            return (await RunQueryAsync<TeamQuery, TeamInfoDto>(_repository, query)).MapTo<TeamInfoViewModel>();
        }

        public ListQueryResult<TeamMemberDto> GetTeamMembersDtos(TeamMembersQuery query)
        {
            return RunListQuery<TeamMembersQuery, TeamMemberDto>(_repository, query);
        }

        public async Task<ListQueryResult<TeamMemberDto>> GetTeamMembersDtosAsync(TeamMembersQuery query)
        {
            return await RunListQueryAsync<TeamMembersQuery, TeamMemberDto>(_repository, query);
        }

        public ListQueryResult<TeamMemberViewModel> GetTeamMembers(TeamMembersQuery query)
        {
            return RunListQuery<TeamMembersQuery, TeamMemberDto>(_repository, query).MapTo<TeamMemberViewModel>();
        }

        public async Task<ListQueryResult<TeamMemberViewModel>> GetTeamMembersAsync(TeamMembersQuery query)
        {
            return (await RunListQueryAsync<TeamMembersQuery, TeamMemberDto>(_repository, query)).MapTo<TeamMemberViewModel>();
        }

        public ListQueryResult<FeatureInfoDto> GetTeamFeaturesDtos(TeamFeaturesQuery query)
        {
            return RunListQuery<TeamFeaturesQuery, FeatureInfoDto>(_repository, query);
        }

        public async Task<ListQueryResult<FeatureInfoDto>> GetTeamFeaturesDtosAsync(TeamFeaturesQuery query)
        {
            return await RunListQueryAsync<TeamFeaturesQuery, FeatureInfoDto>(_repository, query);
        }

        public ListQueryResult<FeatureInfoViewModel> GetTeamFeatures(TeamFeaturesQuery query)
        {
            return RunListQuery<TeamFeaturesQuery, FeatureInfoDto>(_repository, query).MapTo<FeatureInfoViewModel>();
        }

        public async Task<ListQueryResult<FeatureInfoViewModel>> GetTeamFeaturesAsync(TeamFeaturesQuery query)
        {
            return (await RunListQueryAsync<TeamFeaturesQuery, FeatureInfoDto>(_repository, query)).MapTo<FeatureInfoViewModel>();
        }
    }
}