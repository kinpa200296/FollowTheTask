using FollowTheTask.BLL.Services.ActionType.ViewModels;
using FollowTheTask.BLL.Services.Model;
using FollowTheTask.TransferObjects.ActionType.DataObjects;

namespace FollowTheTask.BLL.Services.ActionType
{
    public interface IActionTypeService : IModelService<ActionTypeDto, ActionTypeViewModel>
    {
    }
}