using System.ComponentModel.DataAnnotations;
using FollowTheTask.BLL.Services.Model.ViewModels;

namespace FollowTheTask.BLL.Services.Role.ViewModels
{
    public class RoleViewModel : ModelViewModel
    {
        [Display(Name = "Role")]
        [StringLength(50)]
        public string Name { get; set; }
    }
}