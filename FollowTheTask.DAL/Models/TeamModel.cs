using System.ComponentModel.DataAnnotations.Schema;

namespace FollowTheTask.DAL.Models
{
    [Table("Teams")]
    public class TeamModel : Model
    {
        public string Name { get; set; }

        public int LeaderId { get; set; }
    }
}