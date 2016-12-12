using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using FollowTheTask.DAL.Contexts;
using FollowTheTask.DAL.Models;
using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.Comment.DataObjects;
using FollowTheTask.TransferObjects.Comment.Queries;

namespace FollowTheTask.DAL.Repositories.Comment
{
    public class CommentRepository : ModelRepository<CommentModel, CommentDto>, ICommentRepository
    {
        public CommentRepository(FollowTheTaskContext context) : base(context)
        {
        }


        #region Queries Implementation

        public CommentInfoDto Handle(CommentQuery query)
        {
            return
                Context.Database.SqlQuery<CommentInfoDto>("select * from [dbo].GetComment(@CommentId)",
                    new SqlParameter("CommentId", query.Id)).FirstOrDefault();
        }

        public async Task<CommentInfoDto> HandleAsync(CommentQuery query)
        {
            return
                await Context.Database.SqlQuery<CommentInfoDto>("select * from [dbo].GetComment(@CommentId)",
                    new SqlParameter("CommentId", query.Id)).FirstOrDefaultAsync();
        }

        #endregion Queries Implementation


        #region Commands Implementation

        #endregion Commands Implementation
    }
}