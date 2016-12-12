using FollowTheTask.BLL.Services.Feature.ViewModels;
using FollowTheTask.BLL.Services.Model;
using FollowTheTask.TransferObjects.Feature.DataObjects;

namespace FollowTheTask.BLL.Services.Feature
{
    public interface IFeatureService : IModelService<FeatureDto, FeatureViewModel>
    {
    }
}