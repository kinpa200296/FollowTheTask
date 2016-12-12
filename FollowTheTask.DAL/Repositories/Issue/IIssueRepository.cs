using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.Comment.DataObjects;
using FollowTheTask.TransferObjects.Issue.DataObjects;
using FollowTheTask.TransferObjects.Issue.Queries;

namespace FollowTheTask.DAL.Repositories.Issue
{
    public interface IIssueRepository : IModelRepository<IssueDto>,
        IQueryRepository<IssueQuery, IssueInfoDto>,
        IListQueryRepository<IssueCommentsQuery, CommentInfoDto>
    {
    }
}