using System.Threading.Tasks;
using FollowTheTask.BLL.Result;
using FollowTheTask.BLL.Services.Model;
using FollowTheTask.BLL.Services.Request.ViewModels;
using FollowTheTask.DAL.Repositories.Request;
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
    }
}