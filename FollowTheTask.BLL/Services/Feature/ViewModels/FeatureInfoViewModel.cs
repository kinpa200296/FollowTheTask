using System.ComponentModel.DataAnnotations;
using FollowTheTask.BLL.Services.Model.ViewModels;

namespace FollowTheTask.BLL.Services.Feature.ViewModels
{
    public class FeatureInfoViewModel : ModelViewModel
    {
        [Required]
        [Display(Name = "Name")]
        [StringLength(100)]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [StringLength(2000)]
        public string Description { get; set; }

        [Display(Name = "Version")]
        [StringLength(50)]
        public string Version { get; set; }

        public int TeamId { get; set; }

        [Display(Name = "Team")]
        [StringLength(100)]
        public string Team { get; set; }
    }
}