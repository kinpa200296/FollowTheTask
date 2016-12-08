namespace FollowTheTask.BLL.Result
{
    public class ValueResult<TVal> : ServiceResult<ValueResult<TVal>>
    {
        public TVal Value { get; protected set; }


        public ValueResult() : this(default(TVal), true)
        {
        }

        public ValueResult(TVal value) : this(value, true)
        {
        }

        public ValueResult(TVal value, bool executionCompleted) : base(executionCompleted)
        {
            Value = value;
        }
    }


    public abstract class ValueResult<TVal, TThis> : ServiceResult<TThis>
        where TThis : ValueResult<TVal, TThis>
    {
        public TVal Value { get; protected set; }


        protected ValueResult() : this(default(TVal), true)
        {
        }

        protected ValueResult(TVal value) : this(value, true)
        {
        }

        protected ValueResult(TVal value, bool executionCompleted) : base(executionCompleted)
        {
            Value = value;
        }
    }
}