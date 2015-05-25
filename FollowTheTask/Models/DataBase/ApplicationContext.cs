using System.Data.Entity;
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Manager>()
                .HasRequired<ApplicationUser>(f => f.User)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Worker>()
                .HasRequired<ApplicationUser>(f => f.User)
                .WithMany()
                .WillCascadeOnDelete(false);
        }

        public DbSet<ApplicationRole> IdentityRoles { get; set; }

        public DbSet<Manager> Managers { get; set; }

        public DbSet<Worker> Workers { get; set; }

        public DbSet<TrackedTask> TrackedTasks { get; set; }

        public DbSet<Quest> Quests { get; set; }
    }
}