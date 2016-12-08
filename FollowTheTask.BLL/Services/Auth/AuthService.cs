using FollowTheTask.BLL.Services.Auth.ViewModels;
using FollowTheTask.BLL.Services.Model;
using FollowTheTask.DAL.Repositories.Auth;
using FollowTheTask.TransferObjects.Auth.DataObjects;

namespace FollowTheTask.BLL.Services.Auth
{
    public class AuthService : ModelService<AuthDto, AuthViewModel>, IAuthService
    {
        private readonly IAuthRepository _repository;


        public AuthService(IAuthRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}