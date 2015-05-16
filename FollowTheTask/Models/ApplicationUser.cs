using Microsoft.AspNet.Identity.EntityFramework;

namespace FollowTheTask.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() { }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}