using FollowTheTask.BLL.Services.ActionSource.ViewModels;
using FollowTheTask.BLL.Services.Model;
using FollowTheTask.TransferObjects.ActionSource.DataObjects;

namespace FollowTheTask.BLL.Services.ActionSource
{
    public interface IActionSourceService : IModelService<ActionSourceDto, ActionSourceViewModel>
    {
    }
}