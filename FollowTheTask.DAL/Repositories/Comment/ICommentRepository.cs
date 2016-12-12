using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.Comment.DataObjects;

namespace FollowTheTask.DAL.Repositories.Comment
{
    public interface ICommentRepository : IModelRepository<CommentDto>
    {
    }
}