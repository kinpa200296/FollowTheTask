using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.Role.DataObjects;
using FollowTheTask.TransferObjects.Role.Queries;

namespace FollowTheTask.DAL.Repositories.Role
{
    public interface IRoleRepository : IModelRepository<RoleDto>,
        IQueryRepository<RoleQuery, RoleDto>
    {
    }
}