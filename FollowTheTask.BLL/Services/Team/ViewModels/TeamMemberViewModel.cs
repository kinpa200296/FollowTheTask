using System.ComponentModel.DataAnnotations;
using FollowTheTask.BLL.Services.Model.ViewModels;

namespace FollowTheTask.BLL.Services.Team.ViewModels
{
    public class TeamMemberViewModel : ModelViewModel
    {
        [Display(Name = "Name")]
        [StringLength(120)]
        public string Name { get; set; }
    }
}