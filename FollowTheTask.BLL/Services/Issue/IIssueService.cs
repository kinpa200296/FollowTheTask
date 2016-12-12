using System.Threading.Tasks;
using FollowTheTask.BLL.Result;
using FollowTheTask.BLL.Services.Comment.ViewModels;
using FollowTheTask.BLL.Services.Issue.ViewModels;
using FollowTheTask.BLL.Services.Model;
using FollowTheTask.TransferObjects.Comment.DataObjects;
using FollowTheTask.TransferObjects.Issue.DataObjects;
using FollowTheTask.TransferObjects.Issue.Queries;

namespace FollowTheTask.BLL.Services.Issue
{
    public interface IIssueService : IModelService<IssueDto, IssueViewModel>
    {
        QueryResult<IssueInfoDto> GetIssueDto(IssueQuery query);

        Task<QueryResult<IssueInfoDto>> GetIssueDtoAsync(IssueQuery query);

        QueryResult<IssueInfoViewModel> GetIssue(IssueQuery query);

        Task<QueryResult<IssueInfoViewModel>> GetIssueAsync(IssueQuery query);

        ListQueryResult<CommentInfoDto> GetIssueCommentsDtos(IssueCommentsQuery query);

        Task<ListQueryResult<CommentInfoDto>> GetIssueCommentsDtosAsync(IssueCommentsQuery query);

        ListQueryResult<CommentInfoViewModel> GetIssueComments(IssueCommentsQuery query);

        Task<ListQueryResult<CommentInfoViewModel>> GetIssueCommentsAsync(IssueCommentsQuery query);
    }
}