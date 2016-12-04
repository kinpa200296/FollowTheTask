using System.ComponentModel.DataAnnotations.Schema;

namespace FollowTheTask.DAL.Entities.Issue
{
    [Table("Priorities")]
    public class PriorityEntity : Entity
    {
        public string Name { get; set; }
    }
}