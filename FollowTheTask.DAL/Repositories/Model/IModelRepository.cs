using FollowTheTask.TransferObjects.Model.Commands;
using FollowTheTask.TransferObjects.Model.DataObjects;
using FollowTheTask.TransferObjects.Model.Queries;

namespace FollowTheTask.DAL.Repositories.Model
{
    public interface IModelRepository<TModelDto> : IRepository,
        IListQueryRepository<AllModelsQuery, TModelDto>,
        IQueryRepository<ModelQuery, TModelDto>,
        ICommandRepository<CreateModelCommand<TModelDto>>,
        ICommandRepository<UpdateModelCommand<TModelDto>>,
        ICommandRepository<DeleteModelCommand>
        where TModelDto : ModelDto
    {
    }
}