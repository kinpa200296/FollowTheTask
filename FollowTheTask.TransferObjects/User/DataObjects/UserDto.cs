using FollowTheTask.TransferObjects.Auth.DataObjects;
using FollowTheTask.TransferObjects.Model.DataObjects;

namespace FollowTheTask.TransferObjects.User.DataObjects
{
    public class UserDto : ModelDto
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public AuthDto Auth { get; set; }
    }
}