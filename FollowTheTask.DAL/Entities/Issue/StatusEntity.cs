using System.ComponentModel.DataAnnotations.Schema;

namespace FollowTheTask.DAL.Entities.Issue
{
    [Table("Statuses")]
    public class StatusEntity : Entity
    {
        public string Name { get; set; }
    }
}