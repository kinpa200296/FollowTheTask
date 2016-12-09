using System.Threading.Tasks;
using FollowTheTask.BLL.Result;
using FollowTheTask.BLL.Services.Model;
using FollowTheTask.BLL.Services.Role.ViewModels;
using FollowTheTask.TransferObjects.Role.DataObjects;
using FollowTheTask.TransferObjects.Role.Queries;

namespace FollowTheTask.BLL.Services.Role
{
    public interface IRoleService : IModelService<RoleDto, RoleViewModel>
    {
        QueryResult<RoleDto> GetRoleDto(RoleQuery query);

        Task<QueryResult<RoleDto>> GetRoleDtoAsync(RoleQuery query);

        QueryResult<RoleViewModel> GetRole(RoleQuery query);

        Task<QueryResult<RoleViewModel>> GetRoleAsync(RoleQuery query);
    }
}