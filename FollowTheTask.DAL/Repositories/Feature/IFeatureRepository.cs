using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.Feature.DataObjects;
using FollowTheTask.TransferObjects.Feature.Queries;
using FollowTheTask.TransferObjects.Issue.DataObjects;

namespace FollowTheTask.DAL.Repositories.Feature
{
    public interface IFeatureRepository : IModelRepository<FeatureDto>,
        IQueryRepository<FeatureQuery, FeatureInfoDto>,
        IListQueryRepository<FeatureIssuesQuery, IssueInfoDto>
    {
    }
}