using System.ComponentModel.DataAnnotations;
using FollowTheTask.BLL.Services.Model.ViewModels;

namespace FollowTheTask.BLL.Services.Resolution.ViewModels
{
    public class ResolutionViewModel : ModelViewModel
    {
        [Display(Name = "Resolution")]
        [StringLength(70)]
        public string Name { get; set; }
    }
}