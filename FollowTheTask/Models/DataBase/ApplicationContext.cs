using Microsoft.AspNet.Identity.EntityFramework;

namespace FollowTheTask.Models.DataBase
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext() : base("DefaultConnection") { }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }

        public System.Data.Entity.DbSet<ApplicationRole> IdentityRoles { get; set; }
    }
}