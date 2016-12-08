using FollowTheTask.DAL.Contexts;
using FollowTheTask.DAL.Models;
using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.Auth.DataObjects;

namespace FollowTheTask.DAL.Repositories.Auth
{
    public class AuthRepository : ModelRepository<AuthModel, AuthDto>, IAuthRepository
    {
        public AuthRepository(FollowTheTaskContext context) : base(context)
        {
        }


        #region Queries Implementation

        #endregion Queries Implementation


        #region Commands Implementation

        #endregion Commands Implementation
    }
}