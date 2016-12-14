using System.Threading.Tasks;
using FollowTheTask.BLL.Result;
using FollowTheTask.BLL.Services.Model;
using FollowTheTask.BLL.Services.Role.ViewModels;
using FollowTheTask.BLL.Services.Team.ViewModels;
using FollowTheTask.BLL.Services.User.ViewModels;
using FollowTheTask.TransferObjects.Role.DataObjects;
using FollowTheTask.TransferObjects.Team.DataObjects;
using FollowTheTask.TransferObjects.User.DataObjects;
using FollowTheTask.TransferObjects.User.Queries;

namespace FollowTheTask.BLL.Services.User
{
    public interface IUserService : IModelService<UserDto, UserViewModel>
    {
        QueryResult<UserDto> GetUserDto(UserQuery query);

        Task<QueryResult<UserDto>> GetUserDtoAsync(UserQuery query);

        QueryResult<UserViewModel> GetUser(UserQuery query);

        Task<QueryResult<UserViewModel>> GetUserAsync(UserQuery query);

        ListQueryResult<RoleDto> GetUserRolesDtos(UserRolesQuery query);

        Task<ListQueryResult<RoleDto>> GetUserRolesDtosAsync(UserRolesQuery query);

        ListQueryResult<RoleViewModel> GetUserRoles(UserRolesQuery query);

        Task<ListQueryResult<RoleViewModel>> GetUserRolesAsync(UserRolesQuery query);

        ListQueryResult<TeamInfoDto> GetUserTeamsDtos(UserTeamsQuery query);

        Task<ListQueryResult<TeamInfoDto>> GetUserTeamsDtosAsync(UserTeamsQuery query);

        ListQueryResult<TeamInfoViewModel> GetUserTeams(UserTeamsQuery query);

        Task<ListQueryResult<TeamInfoViewModel>> GetUserTeamsAsync(UserTeamsQuery query);

        ListQueryResult<TeamInfoDto> GetLeaderTeamsDtos(LeaderTeamsQuery query);

        Task<ListQueryResult<TeamInfoDto>> GetLeaderTeamsDtosAsync(LeaderTeamsQuery query);

        ListQueryResult<TeamInfoViewModel> GetLeaderTeams(LeaderTeamsQuery query);

        Task<ListQueryResult<TeamInfoViewModel>> GetLeaderTeamsAsync(LeaderTeamsQuery query);

        QueryResult<CreateIssueAllowedDto> IsCreateIssueAllowed(CreateIssueAllowedQuery query);

        Task<QueryResult<CreateIssueAllowedDto>> IsCreateIssueAllowedAsync(CreateIssueAllowedQuery query);
    }
}