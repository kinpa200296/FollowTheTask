using System.Threading.Tasks;
using FollowTheTask.BLL.Result;
using FollowTheTask.BLL.Services.Comment.ViewModels;
using FollowTheTask.BLL.Services.Model;
using FollowTheTask.DAL.Repositories.Comment;
using FollowTheTask.TransferObjects.Comment.DataObjects;
using FollowTheTask.TransferObjects.Comment.Queries;

namespace FollowTheTask.BLL.Services.Comment
{
    public class CommentService : ModelService<CommentDto, CommentViewModel>, ICommentService
    {
        private readonly ICommentRepository _repository;


        public CommentService(ICommentRepository repository) : base(repository)
        {
            _repository = repository;
        }


        public QueryResult<CommentInfoDto> GetCommentDto(CommentQuery query)
        {
            return RunQuery<CommentQuery, CommentInfoDto>(_repository, query);
        }

        public async Task<QueryResult<CommentInfoDto>> GetCommentDtoAsync(CommentQuery query)
        {
            return await RunQueryAsync<CommentQuery, CommentInfoDto>(_repository, query);
        }

        public QueryResult<CommentInfoViewModel> GetComment(CommentQuery query)
        {
            return RunQuery<CommentQuery, CommentInfoDto>(_repository, query).MapTo<CommentInfoViewModel>();
        }

        public async Task<QueryResult<CommentInfoViewModel>> GetCommentAsync(CommentQuery query)
        {
            return (await RunQueryAsync<CommentQuery, CommentInfoDto>(_repository, query)).MapTo<CommentInfoViewModel>();
        }
    }
}