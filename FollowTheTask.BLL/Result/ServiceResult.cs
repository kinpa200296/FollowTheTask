using System;

namespace FollowTheTask.BLL.Result
{
    public enum SeverityLevel
    {
        Info = 0,
        Warning = 1, 
        Error = 2,
        Fatal = 3
    }


    public abstract class ServiceResult<TThis> where TThis : ServiceResult<TThis>
    {
        public bool ExecutionComleted { get; protected set; }

        public string Message { get; set; }

        public SeverityLevel? Severity { get; set; }

        public Exception Exception { get; set; }

        public bool IsError => Severity > SeverityLevel.Warning;

        public bool IsFailed => !ExecutionComleted || IsError;


        protected ServiceResult() : this(true)
        {
        }

        protected ServiceResult(bool executionCompleted)
        {
            ExecutionComleted = executionCompleted;
            Message = null;
            Severity = null;
            Exception = null;
        }


        public TThis Info(string message, Exception exception = null)
        {
            return SetState(SeverityLevel.Info, message, exception);
        }

        public TThis Warning(string message, Exception exception = null)
        {
            return SetState(SeverityLevel.Warning, message, exception);
        }

        public TThis Error(string message, Exception exception = null)
        {
            return SetState(SeverityLevel.Error, message, exception);
        }

        public TThis Fatal(string message, Exception exception = null)
        {
            return SetState(SeverityLevel.Fatal, message, exception);
        }

        public TThis From<TOther>(ServiceResult<TOther> other) 
            where TOther : ServiceResult<TOther>
        {
            Severity = other.Severity;
            Message = other.Message;
            Exception = other.Exception;
            return (TThis) this;
        }


        private TThis SetState(SeverityLevel severity, string message, Exception exception)
        {
            Severity = severity;
            Message = message;
            Exception = exception;
            return (TThis) this;
        }
    }


    public class ServiceResult : ServiceResult<ServiceResult>
    {
        public ServiceResult() : this(true)
        {
        }

        public ServiceResult(bool executionCompleted) : base(executionCompleted)
        {
        }
    }
}