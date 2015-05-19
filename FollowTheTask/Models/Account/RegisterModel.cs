using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FollowTheTask.Models.Account
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Введите имя пользователя")]
        [Display(Name = "Имя пользователя")]
        [Remote("CheckUserName", "Account", ErrorMessage = "Имя пользователя уже занято")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Введите e-mail")]
        [Display(Name = "E-mail адрес")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный e-mail адрес")]
        [DataType(DataType.EmailAddress)]
        [Remote("CheckEmail", "Account", ErrorMessage = "На данный e-mail уже зарегистрирован пользователь")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [StringLength(40, ErrorMessage = "Длина пароля должна быть не менее {2} и не более {1} символов", MinimumLength = 8)]
        [Display(Name = "Пароль")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z]).*$", ErrorMessage = "Пароль должен состоять из цифр и как минимум одной строчной латинской буквы")]
        public string Password { get; set; }

        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Пароли должны совпадать")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        [Display(Name = "Ваше имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        [Display(Name = "Вашу фамилию")]
        public string LastName { get; set; }

        [Display(Name = "Менеджер")]
        public bool MakeManager { get; set; }

        [Display(Name = "Работник")]
        public bool MakeWorker { get; set; }
    }
}