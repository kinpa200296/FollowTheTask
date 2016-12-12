using FollowTheTask.TransferObjects.Model.DataObjects;

namespace FollowTheTask.TransferObjects.Team.DataObjects
{
    public class TeamInfoDto : ModelDto
    {
        public string Name { get; set; }

        public int LeaderId { get; set; }

        public string Leader { get; set; }
    }
}