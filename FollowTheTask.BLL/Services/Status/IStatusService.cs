using FollowTheTask.BLL.Services.Model;
using FollowTheTask.BLL.Services.Status.ViewModels;
using FollowTheTask.TransferObjects.Status.DataObjects;

namespace FollowTheTask.BLL.Services.Status
{
    public interface IStatusService : IModelService<StatusDto, StatusViewModel>
    {
    }
}