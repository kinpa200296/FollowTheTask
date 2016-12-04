using System.ComponentModel.DataAnnotations.Schema;

namespace FollowTheTask.DAL.Entities
{
    [Table("ActionTypes")]
    public class ActionTypeEntity : Entity
    {
        public string Name { get; set; }
    }
}