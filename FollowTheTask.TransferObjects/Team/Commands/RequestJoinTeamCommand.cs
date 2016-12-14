namespace FollowTheTask.TransferObjects.Team.Commands
{
    public class RequestJoinTeamCommand : Command
    {
        public int UserId { get; set; }

        public int TeamId { get; set; }
    }
}