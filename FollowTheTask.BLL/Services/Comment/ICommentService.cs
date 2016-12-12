using FollowTheTask.BLL.Services.Comment.ViewModels;
using FollowTheTask.BLL.Services.Model;
using FollowTheTask.TransferObjects.Comment.DataObjects;

namespace FollowTheTask.BLL.Services.Comment
{
    public interface ICommentService : IModelService<CommentDto, CommentViewModel>
    {
    }
}