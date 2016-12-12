using FollowTheTask.DAL.Contexts;
using FollowTheTask.DAL.Models;
using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.Priority.DataObjects;

namespace FollowTheTask.DAL.Repositories.Priority
{
    public class PriorityRepository : ModelRepository<PriorityModel, PriorityDto>, IPriorityRepository
    {
        public PriorityRepository(FollowTheTaskContext context) : base(context)
        {
        }


        #region Queries Implementation

        #endregion Queries Implementation


        #region Commands Implementation

        #endregion Commands Implementation
    }
}