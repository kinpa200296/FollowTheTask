using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.Feature.DataObjects;

namespace FollowTheTask.DAL.Repositories.Feature
{
    public interface IFeatureRepository : IModelRepository<FeatureDto>
    {
    }
}