using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FollowTheTask.DAL.Entities
{
    [Table("Users")]
    public class UserEntity : Entity
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int AuthId { get; set; }

        public virtual AuthEntity Auth { get; set; }

        public virtual ICollection<RoleEntity> Roles { get; set; } = new HashSet<RoleEntity>();

        public virtual ICollection<TeamEntity> Teams { get; set; } = new HashSet<TeamEntity>();
    }
}