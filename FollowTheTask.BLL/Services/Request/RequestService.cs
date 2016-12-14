using System.Threading.Tasks;
using FollowTheTask.BLL.Result;
using FollowTheTask.BLL.Services.Model;
using FollowTheTask.BLL.Services.Request.ViewModels;
using FollowTheTask.DAL.Repositories.Request;
using FollowTheTask.TransferObjects.Request.Commands;
using FollowTheTask.TransferObjects.Request.DataObjects;
using FollowTheTask.TransferObjects.Request.Queries;

namespace FollowTheTask.BLL.Services.Request
{
    public class RequestService : ModelService<RequestDto, RequestViewModel>, IRequestService
    {
        private readonly IRequestRepository _repository;


        public RequestService(IRequestRepository repository) : base(repository)
        {
            _repository = repository;
        }


        public QueryResult<RequestInfoDto> GetRequestDto(RequestQuery query)
        {
            return RunQuery<RequestQuery, RequestInfoDto>(_repository, query);
        }

        public async Task<QueryResult<RequestInfoDto>> GetRequestDtoAsync(RequestQuery query)
        {
            return await RunQueryAsync<RequestQuery, RequestInfoDto>(_repository, query);
        }

        public QueryResult<RequestInfoViewModel> GetRequest(RequestQuery query)
        {
            return RunQuery<RequestQuery, RequestInfoDto>(_repository, query).MapTo<RequestInfoViewModel>();
        }

        public async Task<QueryResult<RequestInfoViewModel>> GetRequestAsync(RequestQuery query)
        {
            return (await RunQueryAsync<RequestQuery, RequestInfoDto>(_repository, query)).MapTo<RequestInfoViewModel>();
        }

        public ListQueryResult<RequestInfoDto> GetUserRequestsDtos(UserRequestsQuery query)
        {
            return RunListQuery<UserRequestsQuery, RequestInfoDto>(_repository, query);
        }

        public async Task<ListQueryResult<RequestInfoDto>> GetUserRequestsDtosAsync(UserRequestsQuery query)
        {
            return await RunListQueryAsync<UserRequestsQuery, RequestInfoDto>(_repository, query);
        }

        public ListQueryResult<RequestInfoViewModel> GetUserRequests(UserRequestsQuery query)
        {
            return RunListQuery<UserRequestsQuery, RequestInfoDto>(_repository, query).MapTo<RequestInfoViewModel>();
        }

        public async Task<ListQueryResult<RequestInfoViewModel>> GetUserRequestsAsync(UserRequestsQuery query)
        {
            return (await RunListQueryAsync<UserRequestsQuery, RequestInfoDto>(_repository, query)).MapTo<RequestInfoViewModel>();
        }

        public ListQueryResult<RequestInfoDto> GetUserPendingRequestsDtos(UserPendingRequestsQuery query)
        {
            return RunListQuery<UserPendingRequestsQuery, RequestInfoDto>(_repository, query);
        }

        public async Task<ListQueryResult<RequestInfoDto>> GetUserPendingRequestsDtosAsync(UserPendingRequestsQuery query)
        {
            return await RunListQueryAsync<UserPendingRequestsQuery, RequestInfoDto>(_repository, query);
        }

        public ListQueryResult<RequestInfoViewModel> GetUserPendingRequests(UserPendingRequestsQuery query)
        {
            return RunListQuery<UserPendingRequestsQuery, RequestInfoDto>(_repository, query).MapTo<RequestInfoViewModel>();
        }

        public async Task<ListQueryResult<RequestInfoViewModel>> GetUserPendingRequestsAsync(UserPendingRequestsQuery query)
        {
            return (await RunListQueryAsync<UserPendingRequestsQuery, RequestInfoDto>(_repository, query)).MapTo<RequestInfoViewModel>();
        }

        public CommandResult ApproveRequest(ApproveRequestCommand command)
        {
            return ExecuteCommand(_repository, command);
        }

        public async Task<CommandResult> ApproveRequestAsync(ApproveRequestCommand command)
        {
            return await ExecuteCommandAsync(_repository, command);
        }

        public CommandResult ApprovePendingRequests(ApproveUserRequestsCommand command)
        {
            return ExecuteCommand(_repository, command);
        }

        public async Task<CommandResult> ApprovePendingRequestsAsync(ApproveUserRequestsCommand command)
        {
            return await ExecuteCommandAsync(_repository, command);
        }

        public CommandResult DeclineRequest(DeclineRequestCommand command)
        {
            return ExecuteCommand(_repository, command);
        }

        public async Task<CommandResult> DeclineRequestAsync(DeclineRequestCommand command)
        {
            return await ExecuteCommandAsync(_repository, command);
        }

        public CommandResult DeclinePendingRequests(DeclineUserRequestsCommand command)
        {
            return ExecuteCommand(_repository, command);
        }

        public async Task<CommandResult> DeclinePendingRequestsAsync(DeclineUserRequestsCommand command)
        {
            return await ExecuteCommandAsync(_repository, command);
        }
    }
}