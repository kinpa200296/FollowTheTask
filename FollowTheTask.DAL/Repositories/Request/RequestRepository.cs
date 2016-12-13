using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using FollowTheTask.DAL.Contexts;
using FollowTheTask.DAL.Models;
using FollowTheTask.DAL.Repositories.Model;
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

        #endregion Commands Implementation
    }
}