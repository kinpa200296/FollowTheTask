using System.Linq;
using AutoMapper.QueryableExtensions;
using FollowTheTask.TransferObjects;

namespace FollowTheTask.BLL.Result
{
    public class ListQueryResult<T> : ValueResult<IQueryable<T>, ListQueryResult<T>>
    {
        public Query Query { get; protected set; }


        public ListQueryResult(Query query) : this(query, default(IQueryable<T>), true)
        {
        }

        public ListQueryResult(Query query, IQueryable<T> value) : this(query, value, true)
        {
        }

        public ListQueryResult(Query query, IQueryable<T> value, bool executionCompleted)
            : base(value, executionCompleted)
        {
            Query = query;
        }


        public ListQueryResult<TNew> MapTo<TNew>()
        {
            return new ListQueryResult<TNew>(Query, Value.ProjectTo<TNew>(), ExecutionComleted)
            {
                Message = Message,
                Exception = Exception,
                Severity = Severity
            };
        }
    }
}