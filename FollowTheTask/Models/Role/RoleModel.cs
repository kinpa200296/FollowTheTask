using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FollowTheTask.Models.Role
{
    public class RoleModel
    {
        [Required]
        [RegularExpression("[A-Za-z]+", ErrorMessage = "Только латинские символы")]
        [Remote("CheckName", "Roles", ErrorMessage = "Такая роль уже существует")]
        [Display(Name = "Имя роли в базе данных")]
        public string Name { get; set; }

        [Display(Name = "Возможность удаления")]
        public bool AllowDeletion { get; set; }

        [Required]
        [Display(Name = "Название роли")]
        public string DisplayName { get; set; }

        [Required]
        [Display(Name = "Описание роли")]
        public string Description { get; set; }
    }
}