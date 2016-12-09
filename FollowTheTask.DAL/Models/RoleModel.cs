using System.ComponentModel.DataAnnotations.Schema;

namespace FollowTheTask.DAL.Models
{
    [Table("Roles")]
    public class RoleModel : Model
    {
        public string Name { get; set; }
    }
}