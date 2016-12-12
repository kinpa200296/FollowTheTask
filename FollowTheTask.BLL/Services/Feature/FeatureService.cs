using FollowTheTask.BLL.Services.Feature.ViewModels;
using FollowTheTask.BLL.Services.Model;
using FollowTheTask.DAL.Repositories.Feature;
using FollowTheTask.TransferObjects.Feature.DataObjects;

namespace FollowTheTask.BLL.Services.Feature
{
    public class FeatureService : ModelService<FeatureDto, FeatureViewModel>, IFeatureService
    {
        private readonly IFeatureRepository _repository;


        public FeatureService(IFeatureRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}