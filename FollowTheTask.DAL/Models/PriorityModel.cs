using System.ComponentModel.DataAnnotations.Schema;

namespace FollowTheTask.DAL.Models
{
    [Table("Priorities")]
    public class PriorityModel : Model
    {
        public string Name { get; set; }
    }
}