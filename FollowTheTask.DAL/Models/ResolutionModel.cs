using System.ComponentModel.DataAnnotations.Schema;

namespace FollowTheTask.DAL.Models
{
    [Table("Resolutions")]
    public class ResolutionModel : Model
    {
        public string Name { get; set; }
    }
}