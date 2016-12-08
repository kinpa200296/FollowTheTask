using System.Threading.Tasks;
using FollowTheTask.BLL.Result;
using FollowTheTask.BLL.Services.Model.ViewModels;
using FollowTheTask.TransferObjects.Model.DataObjects;
using FollowTheTask.TransferObjects.Model.Queries;

namespace FollowTheTask.BLL.Services.Model
{
    public interface IModelService<TModelDto, TModelView> : IService
        where TModelDto : ModelDto where TModelView : ModelViewModel
    {
        ListQueryResult<TModelDto> GetAllModelDtos(AllModelsQuery query);

        Task<ListQueryResult<TModelDto>> GetAllModelDtosAsync(AllModelsQuery query);

        ListQueryResult<TModelView> GetAllModels(AllModelsQuery query);

        Task<ListQueryResult<TModelView>> GetAllModelsAsync(AllModelsQuery query);

        QueryResult<TModelDto> GetModelDto(ModelQuery query);

        Task<QueryResult<TModelDto>> GetModelDtoAsync(ModelQuery query);

        QueryResult<TModelView> GetModel(ModelQuery query);

        Task<QueryResult<TModelView>> GetModelAsync(ModelQuery query);

        CommandResult CreateModel(TModelDto dto);

        Task<CommandResult> CreateModelAsync(TModelDto dto);

        CommandResult CreateModel(TModelView viewModel);

        Task<CommandResult> CreateModelAsync(TModelView viewModel);

        CommandResult UpdateModel(TModelDto dto);

        Task<CommandResult> UpdateModelAsync(TModelDto dto);

        CommandResult UpdateModel(TModelView viewModel);

        Task<CommandResult> UpdateModelAsync(TModelView viewModel);

        CommandResult DeleteModel(int id);

        Task<CommandResult> DeleteModelAsync(int id);
    }
}