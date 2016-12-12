using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.Request.DataObjects;

namespace FollowTheTask.DAL.Repositories.Request
{
    public interface IRequestRepository : IModelRepository<RequestDto>
    {
    }
}