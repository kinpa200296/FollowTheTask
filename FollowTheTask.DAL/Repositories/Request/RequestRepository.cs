using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using FollowTheTask.DAL.Contexts;
using FollowTheTask.DAL.Models;
using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.Request.Commands;
using FollowTheTask.TransferObjects.Request.DataObjects;
using FollowTheTask.TransferObjects.Request.Queries;

namespace FollowTheTask.DAL.Repositories.Request
{
    public class RequestRepository : ModelRepository<RequestModel, RequestDto>, IRequestRepository
    {
        public RequestRepository(FollowTheTaskContext context) : base(context)
        {
        }


        #region Queries Implementation

        public RequestInfoDto Handle(RequestQuery query)
        {
            return
                Context.Database.SqlQuery<RequestInfoDto>("select * from [dbo].GetRequest(@RequestId)",
                    new SqlParameter("RequestId", query.Id)).FirstOrDefault();
        }

        public async Task<RequestInfoDto> HandleAsync(RequestQuery query)
        {
            return
                await Context.Database.SqlQuery<RequestInfoDto>("select * from [dbo].GetRequest(@RequestId)",
                        new SqlParameter("RequestId", query.Id)).FirstOrDefaultAsync();
        }

        public IQueryable<RequestInfoDto> Handle(UserRequestsQuery query)
        {
            return
                Context.Database.SqlQuery<RequestInfoDto>("select * from [dbo].GetRequests(@UserId)",
                    new SqlParameter("UserId", query.UserId)).AsQueryable();
        }

        public Task<IQueryable<RequestInfoDto>> HandleAsync(UserRequestsQuery query)
        {
            return
                Task.FromResult(
                    Context.Database.SqlQuery<RequestInfoDto>("select * from [dbo].GetRequests(@UserId)",
                        new SqlParameter("UserId", query.UserId)).AsQueryable());
        }

        public IQueryable<RequestInfoDto> Handle(UserPendingRequestsQuery query)
        {
            return
                Context.Database.SqlQuery<RequestInfoDto>("select * from [dbo].GetPendingRequests(@UserId)",
                    new SqlParameter("UserId", query.UserId)).AsQueryable();
        }

        public Task<IQueryable<RequestInfoDto>> HandleAsync(UserPendingRequestsQuery query)
        {
            return
                Task.FromResult(
                    Context.Database.SqlQuery<RequestInfoDto>("select * from [dbo].GetPendingRequests(@UserId)",
                        new SqlParameter("UserId", query.UserId)).AsQueryable());
        }

        #endregion Queries Implementation


        #region Commands Implementation

        public void Execute(ApproveRequestCommand command)
        {
            Context.Database.ExecuteSqlCommand(TransactionalBehavior.EnsureTransaction,
                "exec [dbo].ApproveRequest @RequestId", new SqlParameter("RequestId", command.RequestId));
        }

        public async Task ExecuteAsync(ApproveRequestCommand command)
        {
            await
                Context.Database.ExecuteSqlCommandAsync(TransactionalBehavior.EnsureTransaction,
                    "exec [dbo].ApproveRequest @RequestId", new SqlParameter("RequestId", command.RequestId));
        }

        public void Execute(ApproveUserRequestsCommand command)
        {
            Context.Database.ExecuteSqlCommand(TransactionalBehavior.EnsureTransaction,
                "exec [dbo].ApproveRequests @UserId", new SqlParameter("UserId", command.UserId));
        }

        public async Task ExecuteAsync(ApproveUserRequestsCommand command)
        {
            await
                Context.Database.ExecuteSqlCommandAsync(TransactionalBehavior.EnsureTransaction,
                    "exec [dbo].ApproveRequests @UserId", new SqlParameter("UserId", command.UserId));
        }

        public void Execute(DeclineRequestCommand command)
        {
            Context.Database.ExecuteSqlCommand(TransactionalBehavior.EnsureTransaction,
                "exec [dbo].DeclineRequest @RequestId", new SqlParameter("RequestId", command.RequestId));
        }

        public async Task ExecuteAsync(DeclineRequestCommand command)
        {
            await
                Context.Database.ExecuteSqlCommandAsync(TransactionalBehavior.EnsureTransaction,
                    "exec [dbo].DeclineRequest @RequestId", new SqlParameter("RequestId", command.RequestId));
        }

        public void Execute(DeclineUserRequestsCommand command)
        {
            Context.Database.ExecuteSqlCommand(TransactionalBehavior.EnsureTransaction,
                "exec [dbo].DeclineRequests @UserId", new SqlParameter("UserId", command.UserId));
        }

        public async Task ExecuteAsync(DeclineUserRequestsCommand command)
        {
            await
                Context.Database.ExecuteSqlCommandAsync(TransactionalBehavior.EnsureTransaction,
                    "exec [dbo].DeclineRequests @UserId", new SqlParameter("UserId", command.UserId));
        }

        #endregion Commands Implementation
    }
}