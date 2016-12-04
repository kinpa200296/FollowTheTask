using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using FollowTheTask.DAL.Entities.Issue;

namespace FollowTheTask.DAL.Entities
{
    [Table("Features")]
    public class FeatureEntity : Entity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Version { get; set; }

        public int TeamId { get; set; }

        public virtual TeamEntity Team { get; set; }

        public virtual ICollection<IssueEntity> Issues { get; set; } = new HashSet<IssueEntity>();
    }
}