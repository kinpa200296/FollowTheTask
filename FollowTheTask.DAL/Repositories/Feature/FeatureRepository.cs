using FollowTheTask.DAL.Contexts;
using FollowTheTask.DAL.Models;
using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.Feature.DataObjects;

namespace FollowTheTask.DAL.Repositories.Feature
{
    public class FeatureRepository : ModelRepository<FeatureModel, FeatureDto>, IFeatureRepository
    {
        public FeatureRepository(FollowTheTaskContext context) : base(context)
        {
        }


        #region Queries Implementation

        #endregion Queries Implementation


        #region Commands Implementation

        #endregion Commands Implementation
    }
}