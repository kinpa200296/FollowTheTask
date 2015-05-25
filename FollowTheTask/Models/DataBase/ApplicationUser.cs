using Microsoft.AspNet.Identity.EntityFramework;

namespace FollowTheTask.Models.DataBase
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() { }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? ManagerId { get; set; }

        public Manager Manager { get; set; }

        public int? WorkerId { get; set; }

        public Worker Worker { get; set; }
    }
}