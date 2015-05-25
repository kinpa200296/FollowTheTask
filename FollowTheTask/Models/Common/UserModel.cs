using System.ComponentModel.DataAnnotations;
using FollowTheTask.Models.DataBase;

namespace FollowTheTask.Models.Common
{
    public class UserModel
    {
        public UserModel()
        {
        }

        public UserModel(ApplicationUser user)
        {
            UserName = user.UserName;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
        }

        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Display(Name = "E-mail адрес")]
        public string Email { get; set; }

        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
    }
}