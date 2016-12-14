namespace FollowTheTask.TransferObjects.Issue.Commands
{
    public class RequestAssignIssueCommand : Command
    {
        public int UserId { get; set; }

        public int IssueId { get; set; }
    }
}