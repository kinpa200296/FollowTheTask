using System;
using System.Linq;
using System.Threading.Tasks;
using FollowTheTask.BLL.Result;
using FollowTheTask.DAL.Repositories;
using FollowTheTask.TransferObjects;

namespace FollowTheTask.BLL.Services
{
    public class Service : IService
    {
        protected bool Disposed { get; private set; }


        protected Service()
        {
            Disposed = false;
        }

        ~Service()
        {
            Dispose(false);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        protected void Dispose(bool disposing)
        {
            if (Disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects.
                DisposeManagedOverride();
            }

            // Free any unmanaged objects.
            DisposeUnManagedOverride();
            Disposed = true;
        }

        protected virtual void DisposeManagedOverride()
        {
        }

        protected virtual void DisposeUnManagedOverride()
        {
        }

        protected QueryResult<TDto> RunQuery<TQuery, TDto>(IQueryRepository<TQuery, TDto> repository, TQuery query)
            where TQuery : Query where TDto : Dto
        {
            TDto queryRes;
            try
            {
                queryRes = repository.Handle(query);
            }
            catch (Exception ex)
            {
                return new QueryResult<TDto>(query, null, false).Fatal($"Unhandled exception: {ex.Message}", ex);
            }
            return queryRes == null
                ? new QueryResult<TDto>(query, null).Warning("Query return nothing")
                : new QueryResult<TDto>(query, queryRes);
        }

        protected async Task<QueryResult<TDto>> RunQueryAsync<TQuery, TDto>(
            IQueryRepository<TQuery, TDto> repository, TQuery query)
            where TQuery : Query where TDto : Dto
        {
            TDto queryRes;
            try
            {
                queryRes = await repository.HandleAsync(query);
            }
            catch (Exception ex)
            {
                return new QueryResult<TDto>(query, null, false).Fatal($"Unhandled exception: {ex.Message}", ex);
            }
            return queryRes == null
                ? new QueryResult<TDto>(query, null).Warning("Query return nothing")
                : new QueryResult<TDto>(query, queryRes);
        }

        protected ListQueryResult<TDto> RunListQuery<TQuery, TDto>(IListQueryRepository<TQuery, TDto> repository, TQuery query)
            where TQuery : Query where TDto : Dto
        {
            IQueryable<TDto> queryRes;
            try
            {
                queryRes = repository.Handle(query);
            }
            catch (Exception ex)
            {
                return new ListQueryResult<TDto>(query, null, false).Fatal($"Unhandled exception: {ex.Message}", ex);
            }
            return queryRes == null
                ? new ListQueryResult<TDto>(query, null).Warning("Query return nothing")
                : new ListQueryResult<TDto>(query, queryRes);
        }

        protected async Task<ListQueryResult<TDto>> RunListQueryAsync<TQuery, TDto>(
            IListQueryRepository<TQuery, TDto> repository, TQuery query)
            where TQuery : Query where TDto : Dto
        {
            IQueryable<TDto> queryRes;
            try
            {
                queryRes = await repository.HandleAsync(query);
            }
            catch (Exception ex)
            {
                return new ListQueryResult<TDto>(query, null, false).Fatal($"Unhandled exception: {ex.Message}", ex);
            }
            return queryRes == null
                ? new ListQueryResult<TDto>(query, null).Warning("Query return nothing")
                : new ListQueryResult<TDto>(query, queryRes);
        }

        protected CommandResult ExecuteCommand<TCommand>(ICommandRepository<TCommand> repository, TCommand command)
            where TCommand : Command
        {
            try
            {
                repository.Execute(command);
            }
            catch (Exception ex)
            {
                return new CommandResult(command, false).Fatal($"Unhandled exception: {ex.Message}", ex);
            }
            return new CommandResult(command);
        }

        protected async Task<CommandResult> ExecuteCommandAsync<TCommand>(
            ICommandRepository<TCommand> repository, TCommand command)
            where TCommand : Command
        {
            try
            {
                await repository.ExecuteAsync(command);
            }
            catch (Exception ex)
            {
                return new CommandResult(command, false).Fatal($"Unhandled exception: {ex.Message}", ex);
            }
            return new CommandResult(command);
        }

        protected ServiceResult InfoResult(string message, Exception exception = null)
        {
            return new ServiceResult(true).Info(message, exception);
        }

        protected ValueResult<T> InfoResult<T>(T val, string message, Exception exception = null)
        {
            return new ValueResult<T>(val, true).Info(message, exception);
        }

        protected ServiceResult WarningResult(string message, Exception exception = null)
        {
            return new ServiceResult(true).Warning(message, exception);
        }

        protected ValueResult<T> WarningResult<T>(T val, string message, Exception exception = null)
        {
            return new ValueResult<T>(val, true).Warning(message, exception);
        }

        protected ServiceResult ErrorResult(string message, Exception exception = null)
        {
            return new ServiceResult(false).Error(message, exception);
        }

        protected ValueResult<T> ErrorResult<T>(T val, string message, Exception exception = null)
        {
            return new ValueResult<T>(val, false).Error(message, exception);
        }

        protected ServiceResult FatalResult(string message, Exception exception = null)
        {
            return new ServiceResult(false).Fatal(message, exception);
        }

        protected ValueResult<T> FatalResult<T>(T val, string message, Exception exception = null)
        {
            return new ValueResult<T>(val, false).Fatal(message, exception);
        }
    }
}