using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.Role.DataObjects;
using FollowTheTask.TransferObjects.Team.DataObjects;
using FollowTheTask.TransferObjects.User.DataObjects;
using FollowTheTask.TransferObjects.User.Queries;

namespace FollowTheTask.DAL.Repositories.User
{
    public interface IUserRepository : IModelRepository<UserDto>,
        IQueryRepository<UserQuery, UserDto>,
        IListQueryRepository<UserRolesQuery, RoleDto>,
        IListQueryRepository<UserTeamsQuery, TeamInfoDto>,
        IListQueryRepository<LeaderTeamsQuery, TeamInfoDto>,
        IQueryRepository<CreateIssueAllowedQuery, CreateIssueAllowedDto>
    {
    }
}