using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.ActionType.DataObjects;

namespace FollowTheTask.DAL.Repositories.ActionType
{
    public interface IActionTypeRepository : IModelRepository<ActionTypeDto>
    {
    }
}