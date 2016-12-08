using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.Auth.DataObjects;

namespace FollowTheTask.DAL.Repositories.Auth
{
    public interface IAuthRepository : IModelRepository<AuthDto>
    {
    }
}