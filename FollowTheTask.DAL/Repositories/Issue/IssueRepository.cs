using FollowTheTask.DAL.Contexts;
using FollowTheTask.DAL.Models;
using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.Issue.DataObjects;

namespace FollowTheTask.DAL.Repositories.Issue
{
    public class IssueRepository : ModelRepository<IssueModel, IssueDto>, IIssueRepository
    {
        public IssueRepository(FollowTheTaskContext context) : base(context)
        {
        }


        #region Queries Implementation

        #endregion Queries Implementation


        #region Commands Implementation

        #endregion Commands Implementation
    }
}