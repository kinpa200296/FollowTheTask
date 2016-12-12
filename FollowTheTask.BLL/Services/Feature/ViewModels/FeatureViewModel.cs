using System.ComponentModel.DataAnnotations;
using FollowTheTask.BLL.Services.Model.ViewModels;

namespace FollowTheTask.BLL.Services.Feature.ViewModels
{
    public class FeatureViewModel : ModelViewModel
    {
        [Display(Name = "Feature Name")]
        [StringLength(100)]
        public string Name { get; set; }

        [Display(Name = "Feature Description")]
        [StringLength(2000)]
        public string Description { get; set; }

        [Display(Name = "Feature Version")]
        [StringLength(50)]
        public string Version { get; set; }

        public int TeamId { get; set; }
    }
}