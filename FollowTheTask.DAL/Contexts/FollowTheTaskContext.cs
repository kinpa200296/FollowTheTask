using System.Data.Entity;
using FollowTheTask.DAL.Models;

namespace FollowTheTask.DAL.Contexts
{
    public class FollowTheTaskContext : DbContext
    {
        public DbSet<AuthModel> AuthData { get; set; }

        public DbSet<UserModel> Users { get; set; }

        public DbSet<TeamModel> Teams { get; set; }

        public DbSet<FeatureModel> Features { get; set; }

        public DbSet<IssueModel> Issues { get; set; }

        public DbSet<CommentModel> Comments { get; set; }

        public DbSet<RequestModel> Requests { get; set; }

        public DbSet<NotificationModel> Notifications { get; set; }

        public DbSet<ActionSourceModel> ActionSources { get; set; }

        public DbSet<ActionTypeModel> ActionTypes { get; set; }

        public DbSet<IssueTypeModel> IssueTypes { get; set; }

        public DbSet<PriorityModel> Priorities { get; set; }

        public DbSet<ResolutionModel> Resolutions { get; set; }

        public DbSet<StatusModel> Statuses { get; set; }


        public FollowTheTaskContext() : base("FollowTheTaskDb")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}