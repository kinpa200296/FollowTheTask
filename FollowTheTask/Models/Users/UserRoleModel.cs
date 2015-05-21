using System.ComponentModel.DataAnnotations;

namespace FollowTheTask.Models.Users
{
    public class UserRoleModel
    {
        [Display(Name = "Имя роли в базе данных")]
        public string Name { get; set; }

        [Display(Name = "Название роли")]
        public string DisplayName { get; set; }

        public bool IsUserInRole { get; set; }
    }
}