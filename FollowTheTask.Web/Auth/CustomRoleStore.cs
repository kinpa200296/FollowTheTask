using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using FollowTheTask.BLL.Result;
using FollowTheTask.BLL.Services.Role;
using FollowTheTask.TransferObjects.Model.Queries;
using FollowTheTask.TransferObjects.Role.Queries;
using Microsoft.AspNet.Identity;

namespace FollowTheTask.Web.Auth
{
    public class CustomRoleStore : IQueryableRoleStore<UserRole, int>
    {
        private readonly IRoleService _roleService;


        public CustomRoleStore()
        {
            _roleService = DependencyResolver.Current.GetService<IRoleService>();
        }


        public void Dispose()
        {
            _roleService.Dispose();
        }

        public IQueryable<UserRole> Roles
            =>
            _roleService.GetAllModelDtos(new AllModelsQuery())?
                .Value.AsEnumerable()
                .Select(UserRole.FromDto)
                .AsQueryable();

        public Task CreateAsync(UserRole role)
        {
            throw new System.NotSupportedException();
        }

        public Task UpdateAsync(UserRole role)
        {
            throw new System.NotSupportedException();
        }

        public Task DeleteAsync(UserRole role)
        {
            throw new System.NotSupportedException();
        }

        public async Task<UserRole> FindByIdAsync(int roleId)
        {
            var result = await _roleService.GetModelDtoAsync(new ModelQuery {Id = roleId});
            return UserRole.FromDto(ProcessQueryResult(result));
        }

        public async Task<UserRole> FindByNameAsync(string roleName)
        {
            var result = await _roleService.GetRoleDtoAsync(new RoleQuery {Name = roleName});
            return UserRole.FromDto(ProcessQueryResult(result));
        }


        private T ProcessQueryResult<T>(QueryResult<T> result)
        {
            if (result.IsFailed)
            {
                Debug.WriteLine(result.Message);
            }
            return result.Value;
        }
    }
}