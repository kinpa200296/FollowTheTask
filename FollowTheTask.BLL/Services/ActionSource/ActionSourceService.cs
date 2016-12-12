using FollowTheTask.BLL.Services.ActionSource.ViewModels;
using FollowTheTask.BLL.Services.Model;
using FollowTheTask.DAL.Repositories.ActionSource;
using FollowTheTask.TransferObjects.ActionSource.DataObjects;

namespace FollowTheTask.BLL.Services.ActionSource
{
    public class ActionSourceService : ModelService<ActionSourceDto, ActionSourceViewModel>, IActionSourceService
    {
        private readonly IActionSourceRepository _repository;


        public ActionSourceService(IActionSourceRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}