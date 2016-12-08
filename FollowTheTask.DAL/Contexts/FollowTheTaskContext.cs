using System.Data.Entity;
using FollowTheTask.DAL.Models;

namespace FollowTheTask.DAL.Contexts
{
    public class FollowTheTaskContext : DbContext
    {
        public DbSet<AuthModel> AuthData { get; set; }

        public DbSet<UserModel> Users { get; set; }


        public FollowTheTaskContext() : base("FollowTheTaskDb")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}