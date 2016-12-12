using FollowTheTask.DAL.Contexts;
using FollowTheTask.DAL.Models;
using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.ActionType.DataObjects;

namespace FollowTheTask.DAL.Repositories.ActionType
{
    public class ActionTypeRepository : ModelRepository<ActionTypeModel, ActionTypeDto>, IActionTypeRepository
    {
        public ActionTypeRepository(FollowTheTaskContext context) : base(context)
        {
        }


        #region Queries Implementation

        #endregion Queries Implementation


        #region Commands Implementation

        #endregion Commands Implementation
    }
}