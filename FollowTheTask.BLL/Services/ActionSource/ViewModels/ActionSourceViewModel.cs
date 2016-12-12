using System.ComponentModel.DataAnnotations;
using FollowTheTask.BLL.Services.Model.ViewModels;

namespace FollowTheTask.BLL.Services.ActionSource.ViewModels
{
    public class ActionSourceViewModel : ModelViewModel
    {
        [Display(Name = "Action source")]
        [StringLength(100)]
        public string Name { get; set; }
    }
}