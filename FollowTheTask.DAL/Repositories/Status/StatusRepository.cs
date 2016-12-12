using FollowTheTask.DAL.Contexts;
using FollowTheTask.DAL.Models;
using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.Status.DataObjects;

namespace FollowTheTask.DAL.Repositories.Status
{
    public class StatusRepository : ModelRepository<StatusModel, StatusDto>, IStatusRepository
    {
        public StatusRepository(FollowTheTaskContext context) : base(context)
        {
        }


        #region Queries Implementation

        #endregion Queries Implementation


        #region Commands Implementation

        #endregion Commands Implementation
    }
}