using System.Threading.Tasks;
using FollowTheTask.BLL.Result;
using FollowTheTask.BLL.Services.Comment.ViewModels;
using FollowTheTask.BLL.Services.Issue.ViewModels;
using FollowTheTask.BLL.Services.Model;
using FollowTheTask.DAL.Repositories.Issue;
using FollowTheTask.TransferObjects.Comment.DataObjects;
using FollowTheTask.TransferObjects.Issue.DataObjects;
using FollowTheTask.TransferObjects.Issue.Queries;

namespace FollowTheTask.BLL.Services.Issue
{
    public class IssueService : ModelService<IssueDto, IssueViewModel>, IIssueService
    {
        private readonly IIssueRepository _repository;


        public IssueService(IIssueRepository repository) : base(repository)
        {
            _repository = repository;
        }


        public QueryResult<IssueInfoDto> GetIssueDto(IssueQuery query)
        {
            return RunQuery<IssueQuery, IssueInfoDto>(_repository, query);
        }

        public async Task<QueryResult<IssueInfoDto>> GetIssueDtoAsync(IssueQuery query)
        {
            return await RunQueryAsync<IssueQuery, IssueInfoDto>(_repository, query);
        }

        public QueryResult<IssueInfoViewModel> GetIssue(IssueQuery query)
        {
            return RunQuery<IssueQuery, IssueInfoDto>(_repository, query).MapTo<IssueInfoViewModel>();
        }

        public async Task<QueryResult<IssueInfoViewModel>> GetIssueAsync(IssueQuery query)
        {
            return (await RunQueryAsync<IssueQuery, IssueInfoDto>(_repository, query)).MapTo<IssueInfoViewModel>();
        }

        public ListQueryResult<CommentInfoDto> GetIssueCommentsDtos(IssueCommentsQuery query)
        {
            return RunListQuery<IssueCommentsQuery, CommentInfoDto>(_repository, query);
        }

        public async Task<ListQueryResult<CommentInfoDto>> GetIssueCommentsDtosAsync(IssueCommentsQuery query)
        {
            return await RunListQueryAsync<IssueCommentsQuery, CommentInfoDto>(_repository, query);
        }

        public ListQueryResult<CommentInfoViewModel> GetIssueComments(IssueCommentsQuery query)
        {
            return RunListQuery<IssueCommentsQuery, CommentInfoDto>(_repository, query).MapTo<CommentInfoViewModel>();
        }

        public async Task<ListQueryResult<CommentInfoViewModel>> GetIssueCommentsAsync(IssueCommentsQuery query)
        {
            return (await RunListQueryAsync<IssueCommentsQuery, CommentInfoDto>(_repository, query)).MapTo<CommentInfoViewModel>();
        }
    }
}