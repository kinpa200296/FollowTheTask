using System.ComponentModel.DataAnnotations.Schema;

namespace FollowTheTask.DAL.Models
{
    [Table("ActionTypes")]
    public class ActionTypeModel : Model
    {
        public string Name { get; set; }
    }
}