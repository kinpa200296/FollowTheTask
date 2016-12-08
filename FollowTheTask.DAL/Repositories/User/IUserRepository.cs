using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.User.DataObjects;
using FollowTheTask.TransferObjects.User.Queries;

namespace FollowTheTask.DAL.Repositories.User
{
    public interface IUserRepository : IModelRepository<UserDto>,
        IQueryRepository<UserQuery, UserDto>
    {
    }
}