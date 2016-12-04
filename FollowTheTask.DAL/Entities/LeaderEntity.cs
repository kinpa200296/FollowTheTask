using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FollowTheTask.DAL.Entities
{
    [Table("Leaders")]
    public class LeaderEntity : Entity
    {
        public int UserId { get; set; }

        public virtual UserEntity User { get; set; }

        public virtual ICollection<TeamEntity> Teams { get; set; } = new HashSet<TeamEntity>();
    }
}