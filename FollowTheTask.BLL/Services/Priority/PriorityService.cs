using FollowTheTask.BLL.Services.Model;
using FollowTheTask.BLL.Services.Priority.ViewModels;
using FollowTheTask.DAL.Repositories.Priority;
using FollowTheTask.TransferObjects.Priority.DataObjects;

namespace FollowTheTask.BLL.Services.Priority
{
    public class PriorityService : ModelService<PriorityDto, PriorityViewModel>, IPriorityService
    {
        private readonly IPriorityRepository _repository;


        public PriorityService(IPriorityRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}