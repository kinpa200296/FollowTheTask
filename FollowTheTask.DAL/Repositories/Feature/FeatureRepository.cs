using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using FollowTheTask.DAL.Contexts;
using FollowTheTask.DAL.Models;
using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.Feature.DataObjects;
using FollowTheTask.TransferObjects.Feature.Queries;
using FollowTheTask.TransferObjects.Issue.DataObjects;

namespace FollowTheTask.DAL.Repositories.Feature
{
    public class FeatureRepository : ModelRepository<FeatureModel, FeatureDto>, IFeatureRepository
    {
        public FeatureRepository(FollowTheTaskContext context) : base(context)
        {
        }


        #region Queries Implementation

        public FeatureInfoDto Handle(FeatureQuery query)
        {
            return
                Context.Database.SqlQuery<FeatureInfoDto>("select * from [dbo].GetFeature(@FeatureId)",
                    new SqlParameter("FeatureId", query.Id)).FirstOrDefault();
        }

        public async Task<FeatureInfoDto> HandleAsync(FeatureQuery query)
        {
            return
                await Context.Database.SqlQuery<FeatureInfoDto>("select * from [dbo].GetFeature(@FeatureId)",
                        new SqlParameter("FeatureId", query.Id)).FirstOrDefaultAsync();
        }

        public IQueryable<IssueInfoDto> Handle(FeatureIssuesQuery query)
        {
            return
                Context.Database.SqlQuery<IssueInfoDto>("select * from [dbo].GetIssues(@FeatureId)",
                    new SqlParameter("FeatureId", query.FeatureId)).AsQueryable();
        }

        public Task<IQueryable<IssueInfoDto>> HandleAsync(FeatureIssuesQuery query)
        {
            return
                Task.FromResult(
                    Context.Database.SqlQuery<IssueInfoDto>("select * from [dbo].GetIssues(@FeatureId)",
                        new SqlParameter("FeatureId", query.FeatureId)).AsQueryable());
        }

        #endregion Queries Implementation


        #region Commands Implementation

        #endregion Commands Implementation
    }
}