using FollowTheTask.BLL.Services.Issue.ViewModels;
using FollowTheTask.BLL.Services.Model;
using FollowTheTask.TransferObjects.Issue.DataObjects;

namespace FollowTheTask.BLL.Services.Issue
{
    public interface IIssueService : IModelService<IssueDto, IssueViewModel>
    {
    }
}