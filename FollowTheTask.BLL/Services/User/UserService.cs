using System.Threading.Tasks;
using FollowTheTask.BLL.Result;
using FollowTheTask.BLL.Services.Model;
using FollowTheTask.BLL.Services.User.ViewModels;
using FollowTheTask.DAL.Repositories.User;
using FollowTheTask.TransferObjects.User.DataObjects;
using FollowTheTask.TransferObjects.User.Queries;

namespace FollowTheTask.BLL.Services.User
{
    public class UserService : ModelService<UserDto, UserViewModel>, IUserService
    {
        private readonly IUserRepository _repository;


        protected UserService(IUserRepository repository) : base(repository)
        {
            _repository = repository;
        }


        public QueryResult<UserDto> GetUserDto(UserQuery query)
        {
            return RunQuery<UserQuery, UserDto>(_repository, query);
        }

        public async Task<QueryResult<UserDto>> GetUserDtoAsync(UserQuery query)
        {
            return await RunQueryAsync<UserQuery, UserDto>(_repository, query);
        }

        public QueryResult<UserViewModel> GetUser(UserQuery query)
        {
            return RunQuery<UserQuery, UserDto>(_repository, query).MapTo<UserViewModel>();
        }

        public async Task<QueryResult<UserViewModel>> GetUserAsync(UserQuery query)
        {
            return (await RunQueryAsync<UserQuery, UserDto>(_repository, query)).MapTo<UserViewModel>();
        }
    }
}