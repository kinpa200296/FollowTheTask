using FollowTheTask.DAL.Contexts;
using FollowTheTask.DAL.Models;
using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.ActionSource.DataObjects;

namespace FollowTheTask.DAL.Repositories.ActionSource
{
    public class ActionSourceRepository : ModelRepository<ActionSourceModel, ActionSourceDto>, IActionSourceRepository
    {
        public ActionSourceRepository(FollowTheTaskContext context) : base(context)
        {
        }


        #region Queries Implementation

        #endregion Queries Implementation


        #region Commands Implementation

        #endregion Commands Implementation
    }
}