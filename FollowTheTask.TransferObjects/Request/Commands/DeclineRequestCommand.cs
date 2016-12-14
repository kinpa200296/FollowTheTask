namespace FollowTheTask.TransferObjects.Request.Commands
{
    public class DeclineRequestCommand : Command
    {
        public int RequestId { get; set; }
    }
}