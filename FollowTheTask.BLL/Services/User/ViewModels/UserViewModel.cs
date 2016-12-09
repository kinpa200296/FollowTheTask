using FollowTheTask.BLL.Services.Auth.ViewModels;
using FollowTheTask.BLL.Services.Model.ViewModels;

namespace FollowTheTask.BLL.Services.User.ViewModels
{
    public class UserViewModel : ModelViewModel
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public AuthViewModel Auth { get; set; }
    }
}