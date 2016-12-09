using System.ComponentModel.DataAnnotations;

namespace FollowTheTask.BLL.Services.User.ViewModels
{
    public class ForgotPasswordViewModel : ViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}