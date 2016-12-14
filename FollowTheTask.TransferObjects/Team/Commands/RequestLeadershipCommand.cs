namespace FollowTheTask.TransferObjects.Team.Commands
{
    public class RequestLeadershipCommand : Command
    {
        public int UserId { get; set; }

        public int TeamId { get; set; }
    }
}