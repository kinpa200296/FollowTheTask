using System.ComponentModel.DataAnnotations;

namespace FollowTheTask.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Введите имя пользователя")]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [StringLength(40, ErrorMessage = "Длина пароля должна быть не менее {2} и не более {1} символов", MinimumLength = 8)]
        [Display(Name = "Пароль")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z]).*$", ErrorMessage = "Пароль должен состоять из цифр и как минимум одной строчной латинской буквы")]
        public string Password { get; set; }
    }
}