using FollowTheTask.BLL.Services.Issue.ViewModels;
using FollowTheTask.BLL.Services.Model;
using FollowTheTask.DAL.Repositories.Issue;
using FollowTheTask.TransferObjects.Issue.DataObjects;

namespace FollowTheTask.BLL.Services.Issue
{
    public class IssueService : ModelService<IssueDto, IssueViewModel>, IIssueService
    {
        private readonly IIssueRepository _repository;


        public IssueService(IIssueRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}