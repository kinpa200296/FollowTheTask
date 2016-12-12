using System.Threading.Tasks;
using FollowTheTask.BLL.Result;
using FollowTheTask.BLL.Services.Comment.ViewModels;
using FollowTheTask.BLL.Services.Model;
using FollowTheTask.TransferObjects.Comment.DataObjects;
using FollowTheTask.TransferObjects.Comment.Queries;

namespace FollowTheTask.BLL.Services.Comment
{
    public interface ICommentService : IModelService<CommentDto, CommentViewModel>
    {
        QueryResult<CommentInfoDto> GetCommentDto(CommentQuery query);

        Task<QueryResult<CommentInfoDto>> GetCommentDtoAsync(CommentQuery query);

        QueryResult<CommentInfoViewModel> GetComment(CommentQuery query);

        Task<QueryResult<CommentInfoViewModel>> GetCommentAsync(CommentQuery query);
    }
}