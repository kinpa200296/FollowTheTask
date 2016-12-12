using FollowTheTask.DAL.Contexts;
using FollowTheTask.DAL.Models;
using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.Request.DataObjects;

namespace FollowTheTask.DAL.Repositories.Request
{
    public class RequestRepository : ModelRepository<RequestModel, RequestDto>, IRequestRepository
    {
        public RequestRepository(FollowTheTaskContext context) : base(context)
        {
        }


        #region Queries Implementation

        #endregion Queries Implementation


        #region Commands Implementation

        #endregion Commands Implementation
    }
}