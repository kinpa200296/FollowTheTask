using System.ComponentModel.DataAnnotations.Schema;

namespace FollowTheTask.DAL.Entities
{
    [Table("ActioSources")]
    public class ActionSourceEntity : Entity
    {
        public string Name { get; set; }
    }
}