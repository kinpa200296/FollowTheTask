using FollowTheTask.BLL.Services.Model;
using FollowTheTask.BLL.Services.Resolution.ViewModels;
using FollowTheTask.TransferObjects.Resolution.DataObjects;

namespace FollowTheTask.BLL.Services.Resolution
{
    public interface IResolutionService : IModelService<ResolutionDto, ResolutionViewModel>
    {
    }
}