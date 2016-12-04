using System.ComponentModel.DataAnnotations.Schema;

namespace FollowTheTask.DAL.Entities.Issue
{
    [Table("Resolutions")]
    public class ResolutionEntity : Entity
    {
        public string Name { get; set; }
    }
}