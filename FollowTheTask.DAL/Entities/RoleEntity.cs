using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FollowTheTask.DAL.Entities
{
    [Table("Roles")]
    public class RoleEntity : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<UserEntity> Users { get; set; } = new HashSet<UserEntity>();
    }
}