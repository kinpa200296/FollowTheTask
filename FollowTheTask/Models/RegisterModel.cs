using System.ComponentModel.DataAnnotations;

namespace FollowTheTask.Models
{
    public class RegisterModel
    {
        [Required]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "E-mail адрес")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли должны совпадать")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        public string PasswordConfirm { get; set; }
    }
}