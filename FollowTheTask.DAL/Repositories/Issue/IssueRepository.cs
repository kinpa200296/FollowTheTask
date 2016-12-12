using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using FollowTheTask.DAL.Contexts;
using FollowTheTask.DAL.Models;
using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.Comment.DataObjects;
using FollowTheTask.TransferObjects.Issue.DataObjects;
using FollowTheTask.TransferObjects.Issue.Queries;

namespace FollowTheTask.DAL.Repositories.Issue
{
    public class IssueRepository : ModelRepository<IssueModel, IssueDto>, IIssueRepository
    {
        public IssueRepository(FollowTheTaskContext context) : base(context)
        {
        }


        #region Queries Implementation

        public IssueInfoDto Handle(IssueQuery query)
        {
            return
                Context.Database.SqlQuery<IssueInfoDto>("select * from [dbo].GetIssue(@IssueId)",
                    new SqlParameter("IssueId", query.Id)).FirstOrDefault();
        }

        public async Task<IssueInfoDto> HandleAsync(IssueQuery query)
        {
            return
                await Context.Database.SqlQuery<IssueInfoDto>("select * from [dbo].GetIssue(@IssueId)",
                        new SqlParameter("IssueId", query.Id)).FirstOrDefaultAsync();
        }

        public IQueryable<CommentInfoDto> Handle(IssueCommentsQuery query)
        {
            return
                Context.Database.SqlQuery<CommentInfoDto>("select * from [dbo].GetComments(@IssueId)",
                    new SqlParameter("IssueId", query.IssueId)).AsQueryable();
        }

        public Task<IQueryable<CommentInfoDto>> HandleAsync(IssueCommentsQuery query)
        {
            return
                Task.FromResult(
                    Context.Database.SqlQuery<CommentInfoDto>("select * from [dbo].GetComments(@IssueId)",
                        new SqlParameter("IssueId", query.IssueId)).AsQueryable());
        }

        #endregion Queries Implementation


        #region Commands Implementation

        #endregion Commands Implementation
    }
}