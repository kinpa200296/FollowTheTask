namespace FollowTheTask.TransferObjects.Request.Commands
{
    public class ApproveUserRequestsCommand : Command
    {
        public int UserId { get; set; }
    }
}