using System.Threading.Tasks;
using AutoMapper;
using FollowTheTask.BLL.Result;
using FollowTheTask.BLL.Services.Model.ViewModels;
using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.Model.Commands;
using FollowTheTask.TransferObjects.Model.DataObjects;
using FollowTheTask.TransferObjects.Model.Queries;

namespace FollowTheTask.BLL.Services.Model
{
    public abstract class ModelService<TModelDto, TModelView> : Service, IModelService<TModelDto, TModelView>
        where TModelDto : ModelDto where TModelView : ModelViewModel
    {
        private readonly IModelRepository<TModelDto> _repository;


        protected ModelService(IModelRepository<TModelDto> repository)
        {
            _repository = repository;
        }


        public ListQueryResult<TModelDto> GetAllModelDtos(AllModelsQuery query)
        {
            return RunListQuery(_repository, query);
        }

        public async Task<ListQueryResult<TModelDto>> GetAllModelDtosAsync(AllModelsQuery query)
        {
            return await RunListQueryAsync(_repository, query);
        }

        public ListQueryResult<TModelView> GetAllModels(AllModelsQuery query)
        {
            return RunListQuery(_repository, query).MapTo<TModelView>();
        }

        public async Task<ListQueryResult<TModelView>> GetAllModelsAsync(AllModelsQuery query)
        {
            return (await RunListQueryAsync(_repository, query)).MapTo<TModelView>();
        }

        public QueryResult<TModelDto> GetModelDto(ModelQuery query)
        {
            return RunQuery(_repository, query);
        }

        public async Task<QueryResult<TModelDto>> GetModelDtoAsync(ModelQuery query)
        {
            return await RunQueryAsync(_repository, query);
        }

        public QueryResult<TModelView> GetModel(ModelQuery query)
        {
            return RunQuery(_repository, query).MapTo<TModelView>();
        }

        public async Task<QueryResult<TModelView>> GetModelAsync(ModelQuery query)
        {
            return (await RunQueryAsync(_repository, query)).MapTo<TModelView>();
        }

        public CommandResult CreateModel(TModelDto dto)
        {
            return ExecuteCommand(_repository, new CreateModelCommand<TModelDto> {ModelDto = dto});
        }

        public async Task<CommandResult> CreateModelAsync(TModelDto dto)
        {
            return await ExecuteCommandAsync(_repository, new CreateModelCommand<TModelDto> {ModelDto = dto});
        }

        public CommandResult CreateModel(TModelView viewModel)
        {
            var command = new CreateModelCommand<TModelDto> {ModelDto = Mapper.Map<TModelDto>(viewModel)};
            var res = ExecuteCommand(_repository, command);
            viewModel.Id = command.ModelDto.Id;
            return res;
        }

        public async Task<CommandResult> CreateModelAsync(TModelView viewModel)
        {
            var command = new CreateModelCommand<TModelDto> {ModelDto = Mapper.Map<TModelDto>(viewModel)};
            var res = await ExecuteCommandAsync(_repository, command);
            viewModel.Id = command.ModelDto.Id;
            return res;
        }

        public CommandResult UpdateModel(TModelDto dto)
        {
            return ExecuteCommand(_repository, new UpdateModelCommand<TModelDto> {ModelDto = dto});
        }

        public async Task<CommandResult> UpdateModelAsync(TModelDto dto)
        {
            return await ExecuteCommandAsync(_repository, new UpdateModelCommand<TModelDto> {ModelDto = dto});
        }

        public CommandResult UpdateModel(TModelView viewModel)
        {
            return ExecuteCommand(_repository,
                new UpdateModelCommand<TModelDto> {ModelDto = Mapper.Map<TModelDto>(viewModel)});
        }

        public async Task<CommandResult> UpdateModelAsync(TModelView viewModel)
        {
            return await ExecuteCommandAsync(_repository,
                new UpdateModelCommand<TModelDto> {ModelDto = Mapper.Map<TModelDto>(viewModel)});
        }

        public CommandResult DeleteModel(int id)
        {
            return ExecuteCommand(_repository, new DeleteModelCommand {Id = id});
        }

        public async Task<CommandResult> DeleteModelAsync(int id)
        {
            return await ExecuteCommandAsync(_repository, new DeleteModelCommand {Id = id});
        }


        protected override void DisposeManagedOverride()
        {
            _repository.Dispose();
        }
    }
}