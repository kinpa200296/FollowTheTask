using System.ComponentModel.DataAnnotations;

namespace FollowTheTask.Models.ManageAccount
{
    public class ChangePasswordModel
    {
        public string UserName { get; set; }

        [Required(ErrorMessage = "Введите старый пароль")]
        [DataType(DataType.Password)]
        [StringLength(40, ErrorMessage = "Длина пароля должна быть не менее {2} и не более {1} символов", MinimumLength = 8)]
        [Display(Name = "Старый пароль")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z]).*$", ErrorMessage = "Пароль должен состоять из цифр и как минимум одной строчной латинской буквы")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Введите новый пароль")]
        [DataType(DataType.Password)]
        [StringLength(40, ErrorMessage = "Длина пароля должна быть не менее {2} и не более {1} символов", MinimumLength = 8)]
        [Display(Name = "Новый пароль")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z]).*$", ErrorMessage = "Пароль должен состоять из цифр и как минимум одной строчной латинской буквы")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Пароли должны совпадать")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение нового пароля")]
        public string PasswordConfirm { get; set; }
    }
}