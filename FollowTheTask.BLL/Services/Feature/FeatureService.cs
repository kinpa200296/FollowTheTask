using System.Threading.Tasks;
using FollowTheTask.BLL.Result;
using FollowTheTask.BLL.Services.Feature.ViewModels;
using FollowTheTask.BLL.Services.Issue.ViewModels;
using FollowTheTask.BLL.Services.Model;
using FollowTheTask.DAL.Repositories.Feature;
using FollowTheTask.TransferObjects.Feature.DataObjects;
using FollowTheTask.TransferObjects.Feature.Queries;
using FollowTheTask.TransferObjects.Issue.DataObjects;

namespace FollowTheTask.BLL.Services.Feature
{
    public class FeatureService : ModelService<FeatureDto, FeatureViewModel>, IFeatureService
    {
        private readonly IFeatureRepository _repository;


        public FeatureService(IFeatureRepository repository) : base(repository)
        {
            _repository = repository;
        }


        public QueryResult<FeatureInfoDto> GetFeatureDto(FeatureQuery query)
        {
            return RunQuery<FeatureQuery, FeatureInfoDto>(_repository, query);
        }

        public async Task<QueryResult<FeatureInfoDto>> GetFeatureDtoAsync(FeatureQuery query)
        {
            return await RunQueryAsync<FeatureQuery, FeatureInfoDto>(_repository, query);
        }

        public QueryResult<FeatureInfoViewModel> GetFeature(FeatureQuery query)
        {
            return RunQuery<FeatureQuery, FeatureInfoDto>(_repository, query).MapTo<FeatureInfoViewModel>();
        }

        public async Task<QueryResult<FeatureInfoViewModel>> GetFeatureAsync(FeatureQuery query)
        {
            return (await RunQueryAsync<FeatureQuery, FeatureInfoDto>(_repository, query)).MapTo<FeatureInfoViewModel>();
        }

        public ListQueryResult<IssueInfoDto> GetFeatureIssuesDto(FeatureIssuesQuery query)
        {
            return RunListQuery<FeatureIssuesQuery, IssueInfoDto>(_repository, query);
        }

        public async Task<ListQueryResult<IssueInfoDto>> GetFeatureIssuesDtoAsync(FeatureIssuesQuery query)
        {
            return await RunListQueryAsync<FeatureIssuesQuery, IssueInfoDto>(_repository, query);
        }

        public ListQueryResult<IssueInfoViewModel> GetFeatureIssues(FeatureIssuesQuery query)
        {
            return RunListQuery<FeatureIssuesQuery, IssueInfoDto>(_repository, query).MapTo<IssueInfoViewModel>();
        }

        public async Task<ListQueryResult<IssueInfoViewModel>> GetFeatureIssuesAsync(FeatureIssuesQuery query)
        {
            return (await RunListQueryAsync<FeatureIssuesQuery, IssueInfoDto>(_repository, query)).MapTo<IssueInfoViewModel>();
        }
    }
}