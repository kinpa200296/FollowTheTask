using AutoMapper;
using FollowTheTask.TransferObjects;

namespace FollowTheTask.BLL.Result
{
    public class QueryResult<T> : ValueResult<T, QueryResult<T>>
    {
        public Query Query { get; protected set; }


        public QueryResult(Query query) : this(query, default(T), true)
        {
        }

        public QueryResult(Query query, T value) : this(query, value, true)
        {
        }

        public QueryResult(Query query, T value, bool executionCompleted) : base(value, executionCompleted)
        {
            Query = query;
        }


        public QueryResult<TNew> MapTo<TNew>()
        {
            return new QueryResult<TNew>(Query, Mapper.Map<TNew>(Value), ExecutionComleted)
            {
                Message = Message,
                Exception = Exception,
                Severity = Severity
            };
        }
    }
}