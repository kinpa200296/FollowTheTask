using System.ComponentModel.DataAnnotations;
using FollowTheTask.BLL.Services.Model.ViewModels;

namespace FollowTheTask.BLL.Services.ActionType.ViewModels
{
    public class ActionTypeViewModel : ModelViewModel
    {
        [Display(Name = "Action type")]
        [StringLength(100)]
        public string Name { get; set; }
    }
}