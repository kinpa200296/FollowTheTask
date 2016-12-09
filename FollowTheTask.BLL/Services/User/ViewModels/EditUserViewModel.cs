using System.ComponentModel.DataAnnotations;

namespace FollowTheTask.BLL.Services.User.ViewModels
{
    public class EditUserViewModel : ViewModel
    {
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(70)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}