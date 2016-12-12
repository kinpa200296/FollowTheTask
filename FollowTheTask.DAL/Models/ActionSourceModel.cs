using System.ComponentModel.DataAnnotations.Schema;

namespace FollowTheTask.DAL.Models
{
    [Table("ActionSources")]
    public class ActionSourceModel : Model
    {
        public string Name { get; set; }
    }
}