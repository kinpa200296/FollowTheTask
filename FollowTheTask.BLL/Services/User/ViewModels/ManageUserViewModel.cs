using System.ComponentModel.DataAnnotations;

namespace FollowTheTask.BLL.Services.User.ViewModels
{
    public class ManageUserViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Email Confirmed")]
        public bool EmailConfirmed { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(70)]
        [Display(Name = "First Name")]
        public string LastName { get; set; }
    }
}