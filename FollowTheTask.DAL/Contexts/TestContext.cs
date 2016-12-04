using System.Data.Entity;
using FollowTheTask.DAL.Entities;
using FollowTheTask.DAL.Entities.Issue;

namespace FollowTheTask.DAL.Contexts
{
    public class TestContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public DbSet<AuthEntity> Auths { get; set; }

        public DbSet<RoleEntity> Roles { get; set; }

        public DbSet<LeaderEntity> Leaders { get; set; }

        public DbSet<TeamEntity> Teams { get; set; }

        public DbSet<FeatureEntity> Features { get; set; }

        public DbSet<IssueTypeEntity> IssueTypes { get; set; }

        public DbSet<PriorityEntity> Priorities { get; set; }

        public DbSet<ResolutionEntity> Resolutions { get; set; }

        public DbSet<StatusEntity> Statuses { get; set; }

        public DbSet<IssueEntity> Issues { get; set; }

        public DbSet<CommentEntity> Comments { get; set; }

        public DbSet<ActionTypeEntity> ActionTypes { get; set; }

        public DbSet<RequestEntity> Requests { get; set; }

        public DbSet<NotificationEntity> Notifications { get; set; }


        public TestContext() : base("TestConnection")
        {
            Database.Initialize(false);
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LeaderEntity>()
                .HasRequired(e => e.User)
                .WithRequiredDependent()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IssueEntity>()
                .HasRequired(e => e.Reporter)
                .WithRequiredDependent()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IssueEntity>()
                .HasRequired(e => e.Assignee)
                .WithRequiredDependent()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FeatureEntity>()
                .HasRequired(e => e.Team)
                .WithRequiredDependent()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NotificationEntity>()
                .HasRequired(e => e.Sender)
                .WithRequiredDependent()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NotificationEntity>()
                .HasRequired(e => e.Receiver)
                .WithRequiredDependent()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RequestEntity>()
                .HasRequired(e => e.Sender)
                .WithRequiredDependent()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RequestEntity>()
                .HasRequired(e => e.Receiver)
                .WithRequiredDependent()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>().MapToStoredProcedures();
            modelBuilder.Entity<AuthEntity>().MapToStoredProcedures();
            modelBuilder.Entity<RoleEntity>().MapToStoredProcedures();
            //modelBuilder.Entity<LeaderEntity>().MapToStoredProcedures();
            modelBuilder.Entity<TeamEntity>().MapToStoredProcedures();
            //modelBuilder.Entity<FeatureEntity>().MapToStoredProcedures();
            modelBuilder.Entity<IssueTypeEntity>().MapToStoredProcedures();
            modelBuilder.Entity<PriorityEntity>().MapToStoredProcedures();
            modelBuilder.Entity<ResolutionEntity>().MapToStoredProcedures();
            modelBuilder.Entity<StatusEntity>().MapToStoredProcedures();
            //modelBuilder.Entity<IssueEntity>().MapToStoredProcedures();
            modelBuilder.Entity<CommentEntity>().MapToStoredProcedures();
            modelBuilder.Entity<ActionTypeEntity>().MapToStoredProcedures();
            //modelBuilder.Entity<RequestEntity>().MapToStoredProcedures();
            //modelBuilder.Entity<NotificationEntity>().MapToStoredProcedures();
        }
    }
}