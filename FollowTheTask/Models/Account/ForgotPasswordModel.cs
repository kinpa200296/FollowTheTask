using System.ComponentModel.DataAnnotations;

namespace FollowTheTask.Models.Account
{
    public class ForgotPasswordModel
    {
        [Required(ErrorMessage = "Введите e-mail")]
        [Display(Name = "E-mail адрес")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный e-mail адрес")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}