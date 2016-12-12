using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.Status.DataObjects;

namespace FollowTheTask.DAL.Repositories.Status
{
    public interface IStatusRepository : IModelRepository<StatusDto>
    {
    }
}