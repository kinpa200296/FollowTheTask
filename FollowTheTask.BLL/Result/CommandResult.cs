using FollowTheTask.TransferObjects;

namespace FollowTheTask.BLL.Result
{
    public class CommandResult : ServiceResult<CommandResult>
    {
        public Command Command { get; protected set; }


        public CommandResult(Command command) : this(command, true)
        {
        }

        public CommandResult(Command command, bool executionCompleted) : base(executionCompleted)
        {
            Command = command;
        }
    }
}