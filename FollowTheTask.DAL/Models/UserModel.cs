using System.ComponentModel.DataAnnotations.Schema;

namespace FollowTheTask.DAL.Models
{
    [Table("Users")]
    public class UserModel : Model
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int AuthId { get; set; }

        public virtual AuthModel Auth { get; set; }


        public UserModel()
        {
            EmailConfirmed = false;
        }
    }
}