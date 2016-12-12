using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using FollowTheTask.DAL.Contexts;
using FollowTheTask.DAL.Models;
using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.Feature.DataObjects;
using FollowTheTask.TransferObjects.Team.DataObjects;
using FollowTheTask.TransferObjects.Team.Queries;

namespace FollowTheTask.DAL.Repositories.Team
{
    public class TeamRepository : ModelRepository<TeamModel, TeamDto>, ITeamRepository
    {
        public TeamRepository(FollowTheTaskContext context) : base(context)
        {
        }


        #region Queries Implementation

        public TeamInfoDto Handle(TeamQuery query)
        {
            return
                Context.Database.SqlQuery<TeamInfoDto>("select * from [dbo].GetTeam(@TeamId)",
                    new SqlParameter("TeamId", query.Id)).FirstOrDefault();
        }

        public async Task<TeamInfoDto> HandleAsync(TeamQuery query)
        {
            return
                await Context.Database.SqlQuery<TeamInfoDto>("select * from [dbo].GetTeam(@TeamId)",
                    new SqlParameter("TeamId", query.Id)).FirstOrDefaultAsync();
        }

        public IQueryable<TeamMemberDto> Handle(TeamMembersQuery query)
        {
            return
                Context.Database.SqlQuery<TeamMemberDto>("select * from [dbo].GetTeamMembers(@TeamId)",
                    new SqlParameter("TeamId", query.TeamId)).AsQueryable();
        }

        public Task<IQueryable<TeamMemberDto>> HandleAsync(TeamMembersQuery query)
        {
            return
                Task.FromResult(
                    Context.Database.SqlQuery<TeamMemberDto>("select * from [dbo].GetTeamMembers(@TeamId)",
                        new SqlParameter("TeamId", query.TeamId)).AsQueryable());
        }

        public IQueryable<FeatureInfoDto> Handle(TeamFeaturesQuery query)
        {
            return
                Context.Database.SqlQuery<FeatureInfoDto>("select * from [dbo].GetFeatures(@TeamId)",
                    new SqlParameter("TeamId", query.TeamId)).AsQueryable();
        }

        public Task<IQueryable<FeatureInfoDto>> HandleAsync(TeamFeaturesQuery query)
        {
            return
                Task.FromResult(
                    Context.Database.SqlQuery<FeatureInfoDto>("select * from [dbo].GetFeatures(@TeamId)",
                        new SqlParameter("TeamId", query.TeamId)).AsQueryable());
        }

        #endregion Queries Implementation


        #region Commands Implementation

        #endregion Commands Implementation
    }
}