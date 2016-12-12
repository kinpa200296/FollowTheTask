using FollowTheTask.DAL.Contexts;
using FollowTheTask.DAL.Models;
using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.IssueType.DataObjects;

namespace FollowTheTask.DAL.Repositories.IssueType
{
    public class IssueTypeRepository : ModelRepository<IssueTypeModel, IssueTypeDto>, IIssueTypeRepository
    {
        public IssueTypeRepository(FollowTheTaskContext context) : base(context)
        {
        }


        #region Queries Implementation

        #endregion Queries Implementation


        #region Commands Implementation

        #endregion Commands Implementation
    }
}