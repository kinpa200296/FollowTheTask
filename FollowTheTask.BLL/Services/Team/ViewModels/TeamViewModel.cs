using System.ComponentModel.DataAnnotations;
using FollowTheTask.BLL.Services.Model.ViewModels;

namespace FollowTheTask.BLL.Services.Team.ViewModels
{
    public class TeamViewModel : ModelViewModel
    {
        [Display(Name = "Name")]
        [StringLength(100)]
        public string Name { get; set; }

        public int LeaderId { get; set; }
    }
}