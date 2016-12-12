using FollowTheTask.DAL.Contexts;
using FollowTheTask.DAL.Models;
using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.Comment.DataObjects;

namespace FollowTheTask.DAL.Repositories.Comment
{
    public class CommentRepository : ModelRepository<CommentModel, CommentDto>, ICommentRepository
    {
        public CommentRepository(FollowTheTaskContext context) : base(context)
        {
        }


        #region Queries Implementation

        #endregion Queries Implementation


        #region Commands Implementation

        #endregion Commands Implementation
    }
}