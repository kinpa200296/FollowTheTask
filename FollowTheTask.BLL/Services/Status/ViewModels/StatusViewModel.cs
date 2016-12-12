using System.ComponentModel.DataAnnotations;
using FollowTheTask.BLL.Services.Model.ViewModels;

namespace FollowTheTask.BLL.Services.Status.ViewModels
{
    public class StatusViewModel : ModelViewModel
    {
        [Display(Name = "Status")]
        [StringLength(70)]
        public string Name { get; set; }
    }
}