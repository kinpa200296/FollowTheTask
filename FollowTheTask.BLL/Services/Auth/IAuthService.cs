using FollowTheTask.BLL.Services.Auth.ViewModels;
using FollowTheTask.BLL.Services.Model;
using FollowTheTask.TransferObjects.Auth.DataObjects;

namespace FollowTheTask.BLL.Services.Auth
{
    public interface IAuthService : IModelService<AuthDto, AuthViewModel>
    {
    }
}