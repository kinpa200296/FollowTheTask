using FollowTheTask.TransferObjects.Model.DataObjects;

namespace FollowTheTask.TransferObjects.Team.DataObjects
{
    public class TeamDto : ModelDto
    {
        public string Name { get; set; }

        public int LeaderId { get; set; }
    }
}