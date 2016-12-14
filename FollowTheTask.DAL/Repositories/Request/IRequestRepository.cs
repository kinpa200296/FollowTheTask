using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.Request.Commands;
using FollowTheTask.TransferObjects.Request.DataObjects;
using FollowTheTask.TransferObjects.Request.Queries;

namespace FollowTheTask.DAL.Repositories.Request
{
    public interface IRequestRepository : IModelRepository<RequestDto>,
        IQueryRepository<RequestQuery, RequestInfoDto>,
        IListQueryRepository<UserRequestsQuery, RequestInfoDto>,
        IListQueryRepository<UserPendingRequestsQuery, RequestInfoDto>,
        ICommandRepository<ApproveRequestCommand>,
        ICommandRepository<ApproveUserRequestsCommand>,
        ICommandRepository<DeclineRequestCommand>,
        ICommandRepository<DeclineUserRequestsCommand>
    {
    }
}