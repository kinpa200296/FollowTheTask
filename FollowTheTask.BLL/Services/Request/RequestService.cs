using FollowTheTask.BLL.Services.Model;
using FollowTheTask.BLL.Services.Request.ViewModels;
using FollowTheTask.DAL.Repositories.Request;
using FollowTheTask.TransferObjects.Request.DataObjects;

namespace FollowTheTask.BLL.Services.Request
{
    public class RequestService : ModelService<RequestDto, RequestViewModel>, IRequestService
    {
        private readonly IRequestRepository _repository;


        public RequestService(IRequestRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}