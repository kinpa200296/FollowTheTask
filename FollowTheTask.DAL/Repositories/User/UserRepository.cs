using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FollowTheTask.DAL.Contexts;
using FollowTheTask.DAL.Models;
using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.Team.DataObjects;
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

        public IQueryable<TeamInfoDto> Handle(UserTeamsQuery query)
        {
            return
                Context.Database.SqlQuery<TeamInfoDto>("select * from [dbo].GetUserTeams(@UserId)",
                    new SqlParameter("UserId", query.UserId)).AsQueryable();
        }

        public Task<IQueryable<TeamInfoDto>> HandleAsync(UserTeamsQuery query)
        {
            return
                Task.FromResult(
                    Context.Database.SqlQuery<TeamInfoDto>("select * from [dbo].GetUserTeams(@UserId)",
                        new SqlParameter("UserId", query.UserId)).AsQueryable());
        }

        public IQueryable<TeamInfoDto> Handle(LeaderTeamsQuery query)
        {
            return
                Context.Database.SqlQuery<TeamInfoDto>("select * from [dbo].GetLeaderTeams(@LeaderId)",
                    new SqlParameter("LeaderId", query.LeaderId)).AsQueryable();
        }

        public Task<IQueryable<TeamInfoDto>> HandleAsync(LeaderTeamsQuery query)
        {
            return
                Task.FromResult(
                    Context.Database.SqlQuery<TeamInfoDto>("select * from [dbo].GetLeaderTeams(@LeaderId)",
                        new SqlParameter("LeaderId", query.LeaderId)).AsQueryable());
        }

        public CreateIssueAllowedDto Handle(CreateIssueAllowedQuery query)
        {
            var allowed =
                Context.Database.SqlQuery<bool>("select[dbo].CanUserCreateIssue(@UserId, @FeatureId)",
                        new SqlParameter("UserId", query.UserId), new SqlParameter("FeatureId", query.FeatureId))
                    .FirstOrDefault();
            return new CreateIssueAllowedDto {Allowed = allowed};
        }

        public async Task<CreateIssueAllowedDto> HandleAsync(CreateIssueAllowedQuery query)
        {
            var allowed =
                await Context.Database.SqlQuery<bool>("select[dbo].CanUserCreateIssue(@UserId, @FeatureId)",
                        new SqlParameter("UserId", query.UserId), new SqlParameter("FeatureId", query.FeatureId))
                    .FirstOrDefaultAsync();
            return new CreateIssueAllowedDto {Allowed = allowed};
        }

        #endregion Queries Implementation


        #region Commands Implementation

        #endregion Commands Implementation
    }
}