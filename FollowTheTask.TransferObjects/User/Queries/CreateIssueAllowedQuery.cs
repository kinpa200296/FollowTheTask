namespace FollowTheTask.TransferObjects.User.Queries
{
    public class CreateIssueAllowedQuery : Query
    {
        public int UserId { get; set; }

        public int FeatureId { get; set; }
    }
}