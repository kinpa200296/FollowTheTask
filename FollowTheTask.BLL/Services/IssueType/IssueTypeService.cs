using FollowTheTask.BLL.Services.IssueType.ViewModels;
using FollowTheTask.BLL.Services.Model;
using FollowTheTask.DAL.Repositories.IssueType;
using FollowTheTask.TransferObjects.IssueType.DataObjects;

namespace FollowTheTask.BLL.Services.IssueType
{
    public class IssueTypeService : ModelService<IssueTypeDto, IssueTypeViewModel>, IIssueTypeService
    {
        private readonly IIssueTypeRepository _repository;


        public IssueTypeService(IIssueTypeRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}