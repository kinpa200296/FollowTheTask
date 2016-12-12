using FollowTheTask.BLL.Services.Model;
using FollowTheTask.BLL.Services.Resolution.ViewModels;
using FollowTheTask.DAL.Repositories.Resolution;
using FollowTheTask.TransferObjects.Resolution.DataObjects;

namespace FollowTheTask.BLL.Services.Resolution
{
    public class ResolutionService : ModelService<ResolutionDto, ResolutionViewModel>, IResolutionService
    {
        private readonly IResolutionRepository _repository;


        public ResolutionService(IResolutionRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}