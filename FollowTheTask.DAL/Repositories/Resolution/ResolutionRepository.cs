using FollowTheTask.DAL.Contexts;
using FollowTheTask.DAL.Models;
using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.Resolution.DataObjects;

namespace FollowTheTask.DAL.Repositories.Resolution
{
    public class ResolutionRepository : ModelRepository<ResolutionModel, ResolutionDto>, IResolutionRepository
    {
        public ResolutionRepository(FollowTheTaskContext context) : base(context)
        {
        }


        #region Queries Implementation

        #endregion Queries Implementation


        #region Commands Implementation

        #endregion Commands Implementation
    }
}