using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.Comment.DataObjects;
using FollowTheTask.TransferObjects.Comment.Queries;

namespace FollowTheTask.DAL.Repositories.Comment
{
    public interface ICommentRepository : IModelRepository<CommentDto>,
        IQueryRepository<CommentQuery, CommentInfoDto>
    {
    }
}