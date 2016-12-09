using System.Threading.Tasks;
using FollowTheTask.BLL.Result;
using FollowTheTask.BLL.Services.Model;
using FollowTheTask.BLL.Services.Role.ViewModels;
using FollowTheTask.DAL.Repositories.Role;
using FollowTheTask.TransferObjects.Role.DataObjects;
using FollowTheTask.TransferObjects.Role.Queries;

namespace FollowTheTask.BLL.Services.Role
{
    public class RoleService : ModelService<RoleDto, RoleViewModel>, IRoleService
    {
        private readonly IRoleRepository _repository;


        public RoleService(IRoleRepository repository) : base(repository)
        {
            _repository = repository;
        }


        public QueryResult<RoleDto> GetRoleDto(RoleQuery query)
        {
            return RunQuery<RoleQuery, RoleDto>(_repository, query);
        }

        public async Task<QueryResult<RoleDto>> GetRoleDtoAsync(RoleQuery query)
        {
            return await RunQueryAsync<RoleQuery, RoleDto>(_repository, query);
        }

        public QueryResult<RoleViewModel> GetRole(RoleQuery query)
        {
            return RunQuery<RoleQuery, RoleDto>(_repository, query).MapTo<RoleViewModel>();
        }

        public async Task<QueryResult<RoleViewModel>> GetRoleAsync(RoleQuery query)
        {
            return (await RunQueryAsync<RoleQuery, RoleDto>(_repository, query)).MapTo<RoleViewModel>();
        }
    }
}