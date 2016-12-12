using FollowTheTask.BLL.Services.Comment.ViewModels;
using FollowTheTask.BLL.Services.Model;
using FollowTheTask.DAL.Repositories.Comment;
using FollowTheTask.TransferObjects.Comment.DataObjects;

namespace FollowTheTask.BLL.Services.Comment
{
    public class CommentService : ModelService<CommentDto, CommentViewModel>, ICommentService
    {
        private readonly ICommentRepository _repository;


        public CommentService(ICommentRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}