using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.Issue.DataObjects;

namespace FollowTheTask.DAL.Repositories.Issue
{
    public interface IIssueRepository : IModelRepository<IssueDto>
    {
    }
}