using System.ComponentModel.DataAnnotations;
using FollowTheTask.BLL.Services.Model.ViewModels;

namespace FollowTheTask.BLL.Services.Priority.ViewModels
{
    public class PriorityViewModel : ModelViewModel
    {
        [Display(Name = "Priority")]
        [StringLength(70)]
        public string Name { get; set; }
    }
}