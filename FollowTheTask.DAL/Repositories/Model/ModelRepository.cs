using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FollowTheTask.DAL.Contexts;
using FollowTheTask.TransferObjects.Model.Commands;
using FollowTheTask.TransferObjects.Model.DataObjects;
using FollowTheTask.TransferObjects.Model.Queries;

namespace FollowTheTask.DAL.Repositories.Model
{
    public abstract class ModelRepository<TModel, TModelDto> : Repository, IModelRepository<TModelDto>
        where TModel : Models.Model, new() where TModelDto : ModelDto
    {
        protected readonly DbSet<TModel> ModelsDao;


        protected ModelRepository(FollowTheTaskContext context) : base(context)
        {
            ModelsDao = Context.Set<TModel>();
        }


        #region Queries Implementation

        public IQueryable<TModelDto> Handle(AllModelsQuery query)
        {
            return ModelsDao.ProjectTo<TModelDto>();
        }

        public Task<IQueryable<TModelDto>> HandleAsync(AllModelsQuery query)
        {
            return Task.FromResult(ModelsDao.ProjectTo<TModelDto>());
        }

        public TModelDto Handle(ModelQuery query)
        {
            return Mapper.Map<TModelDto>(ModelsDao.Find(query.Id));
        }

        public async Task<TModelDto> HandleAsync(ModelQuery query)
        {
            return Mapper.Map<TModelDto>(await ModelsDao.FindAsync(query.Id));
        }

        #endregion Queries Implementation


        #region Commands Implementation

        public void Execute(CreateModelCommand<TModelDto> command)
        {
            var model = Mapper.Map<TModel>(command.ModelDto);
            ModelsDao.Add(model);
            Save();
            command.ModelDto.Id = model.Id;
        }

        public async Task ExecuteAsync(CreateModelCommand<TModelDto> command)
        {
            var model = Mapper.Map<TModel>(command.ModelDto);
            ModelsDao.Add(model);
            await SaveAsync();
            command.ModelDto.Id = model.Id;
        }

        public void Execute(UpdateModelCommand<TModelDto> command)
        {
            // if we requested a model with certain then
            // context already has an attached model with this id
            // therefore we have to find it or we will get key conflict
            var model = ModelsDao.Find(command.ModelDto.Id);
            if (model == null)
            {
                throw new ArgumentException("Model with given Id not found");
            }
            Mapper.Map(command.ModelDto, model);
            Context.Entry(model).State = EntityState.Modified;
            Save();
        }

        public async Task ExecuteAsync(UpdateModelCommand<TModelDto> command)
        {
            var model = await ModelsDao.FindAsync(command.ModelDto.Id);
            if (model == null)
            {
                throw new ArgumentException("Model with given Id not found");
            }
            Mapper.Map(command.ModelDto, model);
            Context.Entry(model).State = EntityState.Modified;
            await SaveAsync();
        }

        public void Execute(DeleteModelCommand command)
        {
            var model = ModelsDao.Find(command.Id);
            if (model == null)
            {
                throw new ArgumentException("Model with given Id not found");
            }
            ModelsDao.Remove(model);
            Save();
        }

        public async Task ExecuteAsync(DeleteModelCommand command)
        {
            var model = await ModelsDao.FindAsync(command.Id);
            if (model == null)
            {
                throw new ArgumentException("Model with given Id not found");
            }
            ModelsDao.Remove(model);
            await SaveAsync();
        }

        #endregion Commands Implementation
    }
}