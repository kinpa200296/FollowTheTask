using System.ComponentModel.DataAnnotations;

namespace FollowTheTask.Models.Users
{
    public class UserModel
    {
        public string Id { get; set; }

        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Display(Name = "E-mail адрес")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Роли")]
        public UserRoleModel[] Roles { get; set; }
    }
}