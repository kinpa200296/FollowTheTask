using System.Threading.Tasks;
using FollowTheTask.BLL.Result;
using FollowTheTask.BLL.Services.Model;
using FollowTheTask.BLL.Services.Request.ViewModels;
using FollowTheTask.TransferObjects.Request.Commands;
using FollowTheTask.TransferObjects.Request.DataObjects;
using FollowTheTask.TransferObjects.Request.Queries;

namespace FollowTheTask.BLL.Services.Request
{
    public interface IRequestService : IModelService<RequestDto, RequestViewModel>
    {
        QueryResult<RequestInfoDto> GetRequestDto(RequestQuery query);

        Task<QueryResult<RequestInfoDto>> GetRequestDtoAsync(RequestQuery query);

        QueryResult<RequestInfoViewModel> GetRequest(RequestQuery query);

        Task<QueryResult<RequestInfoViewModel>> GetRequestAsync(RequestQuery query);

        ListQueryResult<RequestInfoDto> GetUserRequestsDtos(UserRequestsQuery query);

        Task<ListQueryResult<RequestInfoDto>> GetUserRequestsDtosAsync(UserRequestsQuery query);

        ListQueryResult<RequestInfoViewModel> GetUserRequests(UserRequestsQuery query);

        Task<ListQueryResult<RequestInfoViewModel>> GetUserRequestsAsync(UserRequestsQuery query);

        ListQueryResult<RequestInfoDto> GetUserPendingRequestsDtos(UserPendingRequestsQuery query);

        Task<ListQueryResult<RequestInfoDto>> GetUserPendingRequestsDtosAsync(UserPendingRequestsQuery query);

        ListQueryResult<RequestInfoViewModel> GetUserPendingRequests(UserPendingRequestsQuery query);

        Task<ListQueryResult<RequestInfoViewModel>> GetUserPendingRequestsAsync(UserPendingRequestsQuery query);

        CommandResult ApproveRequest(ApproveRequestCommand command);

        Task<CommandResult> ApproveRequestAsync(ApproveRequestCommand command);

        CommandResult ApprovePendingRequests(ApproveUserRequestsCommand command);

        Task<CommandResult> ApprovePendingRequestsAsync(ApproveUserRequestsCommand command);

        CommandResult DeclineRequest(DeclineRequestCommand command);

        Task<CommandResult> DeclineRequestAsync(DeclineRequestCommand command);

        CommandResult DeclinePendingRequests(DeclineUserRequestsCommand command);

        Task<CommandResult> DeclinePendingRequestsAsync(DeclineUserRequestsCommand command);
    }
}