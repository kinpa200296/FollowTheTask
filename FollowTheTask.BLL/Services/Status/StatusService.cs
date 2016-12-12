using FollowTheTask.BLL.Services.Model;
using FollowTheTask.BLL.Services.Status.ViewModels;
using FollowTheTask.DAL.Repositories.Status;
using FollowTheTask.TransferObjects.Status.DataObjects;

namespace FollowTheTask.BLL.Services.Status
{
    public class StatusService : ModelService<StatusDto, StatusViewModel>, IStatusService
    {
        private readonly IStatusRepository _repository;


        public StatusService(IStatusRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}