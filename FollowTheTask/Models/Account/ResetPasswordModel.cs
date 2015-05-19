using System.ComponentModel.DataAnnotations;

namespace FollowTheTask.Models.Account
{
    public class ResetPasswordModel
    {
        public string UserId { get; set; }

        public string Token { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [StringLength(40, ErrorMessage = "Длина пароля должна быть не менее {2} и не более {1} символов", MinimumLength = 8)]
        [Display(Name = "Пароль")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z]).*$", ErrorMessage = "Пароль должен состоять из цифр и как минимум одной строчной латинской буквы")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Пароли должны совпадать")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        public string PasswordConfirm { get; set; }
    }
}