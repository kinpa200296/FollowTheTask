using FollowTheTask.BLL.Services.Model;
using FollowTheTask.BLL.Services.Request.ViewModels;
using FollowTheTask.TransferObjects.Request.DataObjects;

namespace FollowTheTask.BLL.Services.Request
{
    public interface IRequestService : IModelService<RequestDto, RequestViewModel>
    {
    }
}