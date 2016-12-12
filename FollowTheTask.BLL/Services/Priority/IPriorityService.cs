using FollowTheTask.BLL.Services.Model;
using FollowTheTask.BLL.Services.Priority.ViewModels;
using FollowTheTask.TransferObjects.Priority.DataObjects;

namespace FollowTheTask.BLL.Services.Priority
{
    public interface IPriorityService : IModelService<PriorityDto, PriorityViewModel>
    {
    }
}