using System.ComponentModel.DataAnnotations;
using FollowTheTask.BLL.Services.Auth.ViewModels;
using FollowTheTask.BLL.Services.Model.ViewModels;

namespace FollowTheTask.BLL.Services.User.ViewModels
{
    public class UserViewModel : ModelViewModel
    {
        [Display(Name = "Username")]
        [StringLength(50)]
        public string Username { get; set; }

        [Display(Name = "Email")]
        [StringLength(70)]
        public string Email { get; set; }

        [Display(Name = "Email Confirmed")]
        public bool EmailConfirmed { get; set; }

        [Display(Name = "First Name")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(70)]
        public string LastName { get; set; }

        public AuthViewModel Auth { get; set; }
    }
}