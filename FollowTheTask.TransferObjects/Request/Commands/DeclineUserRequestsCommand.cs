namespace FollowTheTask.TransferObjects.Request.Commands
{
    public class DeclineUserRequestsCommand : Command
    {
        public int UserId { get; set; }
    }
}