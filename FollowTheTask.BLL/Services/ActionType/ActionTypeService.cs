using FollowTheTask.BLL.Services.ActionType.ViewModels;
using FollowTheTask.BLL.Services.Model;
using FollowTheTask.DAL.Repositories.ActionType;
using FollowTheTask.TransferObjects.ActionType.DataObjects;

namespace FollowTheTask.BLL.Services.ActionType
{
    public class ActionTypeService : ModelService<ActionTypeDto, ActionTypeViewModel>, IActionTypeService
    {
        private readonly IActionTypeRepository _repository;


        public ActionTypeService(IActionTypeRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}