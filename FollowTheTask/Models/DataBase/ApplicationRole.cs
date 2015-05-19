using Microsoft.AspNet.Identity.EntityFramework;

namespace FollowTheTask.Models.DataBase
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() { }

        public bool AllowDeletion { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }
    }
}