using System.Threading.Tasks;
using FollowTheTask.TransferObjects;

namespace FollowTheTask.DAL.Repositories
{
    public interface IQueryRepository<TQuery, TDto> where TQuery : Query where TDto : Dto
    {
        TDto Handle(TQuery query);

        Task<TDto> HandleAsync(TQuery query);
    }
}