using System.ComponentModel.DataAnnotations.Schema;

namespace FollowTheTask.DAL.Models
{
    [Table("Statuses")]
    public class StatusModel : Model
    {
        public string Name { get; set; }
    }
}