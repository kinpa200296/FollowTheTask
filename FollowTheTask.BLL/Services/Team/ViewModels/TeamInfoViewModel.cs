using System.ComponentModel.DataAnnotations;
using FollowTheTask.BLL.Services.Model.ViewModels;

namespace FollowTheTask.BLL.Services.Team.ViewModels
{
    public class TeamInfoViewModel : ModelViewModel
    {
        [Required]
        [Display(Name = "Name")]
        [StringLength(100)]
        public string Name { get; set; }

        public int LeaderId { get; set; }

        [Display(Name = "Leader")]
        [StringLength(120)]
        public string Leader { get; set; }
    }
}