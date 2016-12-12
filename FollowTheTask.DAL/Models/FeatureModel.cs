using System.ComponentModel.DataAnnotations.Schema;

namespace FollowTheTask.DAL.Models
{
    [Table("Features")]
    public class FeatureModel : Model
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Version { get; set; }

        public int TeamId { get; set; }
    }
}