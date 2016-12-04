using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FollowTheTask.DAL.Entities
{
    [Table("Teams")]
    public class TeamEntity : Entity
    {
        public string Name { get; set; }

        public int LeaderId { get; set; }

        public virtual LeaderEntity Leader { get; set; }

        public virtual ICollection<UserEntity> Members { get; set; } = new HashSet<UserEntity>();

        public virtual ICollection<FeatureEntity> Features { get; set; } = new HashSet<FeatureEntity>();
    }
}