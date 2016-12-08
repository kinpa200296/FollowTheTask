using System.Threading.Tasks;
using FollowTheTask.BLL.Result;
using FollowTheTask.BLL.Services.Model;
using FollowTheTask.BLL.Services.User.ViewModels;
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
    }
}