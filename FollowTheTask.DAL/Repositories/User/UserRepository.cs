using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FollowTheTask.DAL.Contexts;
using FollowTheTask.DAL.Models;
using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.User.DataObjects;
using FollowTheTask.TransferObjects.User.Queries;

namespace FollowTheTask.DAL.Repositories.User
{
    public class UserRepository : ModelRepository<UserModel, UserDto>, IUserRepository
    {
        public UserRepository(FollowTheTaskContext context) : base(context)
        {
        }


        #region Queries Implementation

        #endregion Queries Implementation


        #region Commands Implementation

        public UserDto Handle(UserQuery query)
        {
            UserModel model = null;
            if (query.Id.HasValue)
            {
                model = ModelsDao.Find(query.Id.Value);
            }
            if (!string.IsNullOrEmpty(query.Email))
            {
                model = ModelsDao.FirstOrDefault(u => u.Email == query.Email);
            }
            if (!string.IsNullOrEmpty(query.Username))
            {
                model = ModelsDao.FirstOrDefault(u => u.Username == query.Username);
            }
            return model == null ? null : Mapper.Map<UserDto>(model);
        }

        public async Task<UserDto> HandleAsync(UserQuery query)
        {
            UserModel model = null;
            if (query.Id.HasValue)
            {
                model = await ModelsDao.FindAsync(query.Id.Value);
            }
            if (!string.IsNullOrEmpty(query.Email))
            {
                model = await ModelsDao.FirstOrDefaultAsync(u => u.Email == query.Email);
            }
            if (!string.IsNullOrEmpty(query.Username))
            {
                model = await ModelsDao.FirstOrDefaultAsync(u => u.Username == query.Username);
            }
            return model == null ? null : Mapper.Map<UserDto>(model);
        }

        #endregion Commands Implementation
    }
}