using System.Threading.Tasks;
using FollowTheTask.BLL.Result;
using FollowTheTask.BLL.Services.Feature.ViewModels;
using FollowTheTask.BLL.Services.Issue.ViewModels;
using FollowTheTask.BLL.Services.Model;
using FollowTheTask.TransferObjects.Feature.DataObjects;
using FollowTheTask.TransferObjects.Feature.Queries;
using FollowTheTask.TransferObjects.Issue.DataObjects;

namespace FollowTheTask.BLL.Services.Feature
{
    public interface IFeatureService : IModelService<FeatureDto, FeatureViewModel>
    {
        QueryResult<FeatureInfoDto> GetFeatureDto(FeatureQuery query);

        Task<QueryResult<FeatureInfoDto>> GetFeatureDtoAsync(FeatureQuery query);

        QueryResult<FeatureInfoViewModel> GetFeature(FeatureQuery query);

        Task<QueryResult<FeatureInfoViewModel>> GetFeatureAsync(FeatureQuery query);

        ListQueryResult<IssueInfoDto> GetFeatureIssuesDto(FeatureIssuesQuery query);

        Task<ListQueryResult<IssueInfoDto>> GetFeatureIssuesDtoAsync(FeatureIssuesQuery query);

        ListQueryResult<IssueInfoViewModel> GetFeatureIssues(FeatureIssuesQuery query);

        Task<ListQueryResult<IssueInfoViewModel>> GetFeatureIssuesAsync(FeatureIssuesQuery query);
    }
}