using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FollowTheTask.BLL.Services.Model.ViewModels;

namespace FollowTheTask.BLL.Services.User.ViewModels
{
    public class RegisterViewModel : ModelViewModel
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "Username")]
        [Remote("CheckUserName", "Account", ErrorMessage = "Username is already taken")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [Remote("CheckEmail", "Account", ErrorMessage = "Email is already taken")]
        public string Email { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

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