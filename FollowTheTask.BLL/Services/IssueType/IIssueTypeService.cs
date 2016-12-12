using FollowTheTask.BLL.Services.IssueType.ViewModels;
using FollowTheTask.BLL.Services.Model;
using FollowTheTask.TransferObjects.IssueType.DataObjects;

namespace FollowTheTask.BLL.Services.IssueType
{
    public interface IIssueTypeService : IModelService<IssueTypeDto, IssueTypeViewModel>
    {
    }
}